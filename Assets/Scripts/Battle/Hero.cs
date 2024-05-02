using UnityEngine;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class HeroData
{
    public int hp;
    public int maxHp;
    public int shield;
    public int maxShield;
    public int startHp;
    public int dame;
    public int level;
    public float currentExp;
}

[System.Serializable]
public class DamageData
{
    public int damage;
}

public class Hero : MonoBehaviour
{
    public const float EXP_TO_LEVEL_UP = 3f;

    public HeroData heroData;
    private TextHp textHp;
    private TextShield textShield;
    [SerializeField] TextLevelUp textLevelUp;

    private void Awake()
    {
        heroData = new HeroData();
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
            if (heroData.hp < 0)
            {
                Debug.Log("Die!");
                heroData.hp = 0;
                dead = true;
            }
        }
        else
        {
            heroData.shield -= data.damage;
        }
        textHp.SetHP(heroData);
        textShield.SetShield(heroData);
        dead = false;
    }

    public void AddEXP(float exp)
    {
        heroData.currentExp += exp;
        if (heroData.currentExp > EXP_TO_LEVEL_UP)
        {
            int oldLevel = heroData.level;
            heroData.currentExp -= EXP_TO_LEVEL_UP;
            heroData.level++;
            SimpleObjectPool.Instance.GetObjectFromPool(textLevelUp, transform.position);
            DOVirtual.DelayedCall(0.5f, () =>
            {
                PopupManager.Instance.ShowInQueue(() =>
                {
                    GameFlow.Instance.popupLevelUp.ShowLevelUp(oldLevel);
                });
                if (heroData.level == 2)
                {
                    PopupManager.Instance.ShowInQueue(() =>
                    {
                        GameFlow.Instance.popupPoweupUnlocked.ShowUnlock(PowerupId.Slash);
                    });
                }
            });
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
    }

    public int GetHP()
    {
        return heroData.hp;
    }
}