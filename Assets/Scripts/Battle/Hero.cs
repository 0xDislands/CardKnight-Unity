using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

[System.Serializable]
public class HeroBattleData
{
    public int hp;
    public int maxHp;
    public int shield;
    public int maxShield;
    public int startHp;
    public int dame;
    public int level;
    public float currentExp;
    public float totalExp;
}

[System.Serializable]
public class DamageData
{
    public int damage;

    public DamageData() { }
    public DamageData(int damage)
    {
        this.damage = damage;
    }
}

public class Hero : MonoBehaviour
{
    public const float EXP_TO_LEVEL_UP = 3f;

    public HeroBattleData heroData;
    private TextHp textHp;
    private TextShield textShield;
    [SerializeField] TextLevelUp textLevelUp;

    private void Awake()
    {
        heroData = new HeroBattleData();
        textHp = GetComponentInChildren<TextHp>();
        textShield = GetComponentInChildren<TextShield>();
        heroData.hp = 10;
        heroData.maxHp = 10;
        heroData.shield = 0;
        heroData.maxShield = 10;
    }
    private void Start()
    {
        textHp.SetHP(heroData);
        textShield.SetShield(heroData);
    }

    public void TakeDamage(DamageData data, out bool dead)
    {
        if (data.damage > heroData.shield)
        {
            heroData.hp -= (data.damage - heroData.shield);
            heroData.shield = 0;
            if (heroData.hp <= 0)
            {
                Debug.Log("Die!");
                heroData.hp = 0;
                dead = true;
                Gameplay.Instance.Lose();
            }
        }
        else
        {
            heroData.shield -= data.damage;
        }
        textHp.SetHP(heroData);
        textShield.SetShield(heroData);
        dead = false;
        EffectManager.Instance.Hit(transform.position);
    }

    public void AddEXP(float exp)
    {
        heroData.currentExp += exp;
        heroData.totalExp += exp;
        if (heroData.currentExp > EXP_TO_LEVEL_UP)
        {
            int oldLevel = heroData.level;
            heroData.currentExp -= EXP_TO_LEVEL_UP;
            heroData.level++;
            Gameplay.Instance.heroProgressBarExp.SetValue(0);
            SimpleObjectPool.Instance.GetObjectFromPool(textLevelUp, transform.position);

            if (Gameplay.Instance.state == GameplayState.Lose ||
                Gameplay.Instance.state == GameplayState.Win)
            {
                return;
            }

            DOVirtual.DelayedCall(0.5f, () =>
            {
                PopupManager.Instance.ShowInQueue(() =>
                {
                    Gameplay.Instance.popupLevelUp.ShowLevelUp(oldLevel);
                });
                UnlockSkills();
            });
        }
    }

    private void UnlockSkills()
    {
        var dataManager = DataManager.Instance;
        var powerUpDatas = DataManager.Instance.dicHero[CardManager.selectedHero].powerUps;
        for (int i = 0; i < powerUpDatas.Count; i++)
        {
            var id = powerUpDatas[i];

            if (heroData.level == dataManager.dicPowerUp[id].unlockLevel)
            {
                PopupManager.Instance.ShowInQueue(() =>
                {
                    Gameplay.Instance.popupPoweupUnlocked.ShowUnlock(id);
                });
            }
        }
    }

    public void AddHP(DamageData data)
    {
        heroData.hp += data.damage;
        if (heroData.hp > heroData.maxHp)
        {
            heroData.hp = heroData.maxHp;
        }
        if (heroData.hp < 0)
        {
            heroData.hp = 0;
            Die();
        }
        textHp.SetHP(heroData);
    }

    public void AddShield(int amount)
    {
        heroData.shield += amount;
        if (heroData.shield > heroData.maxShield)
        {
            heroData.shield = heroData.maxShield;
        }
        textShield.SetShield(heroData);
    }
    public void UpdateDisplay ()
    {
        textHp.SetHP (heroData);
        textShield.SetShield (heroData);
    }

    public void Die()
    {
        Debug.Log("game over!");
        Gameplay.Instance.Lose();
    }

    public int GetHP()
    {
        return heroData.hp;
    }
}