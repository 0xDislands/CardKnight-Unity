using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class HeroBattleData
{
    public int hp;
    public int maxHp;
    public float shield;
    public float maxShield;
    public int startHp;
    public int damage;
    public int level;
    public float currentExp;
    public float totalExp;
}

[System.Serializable]
public class DamageData
{
    public float damage;

    public DamageData() { }
    public DamageData(float damage)
    {
        this.damage = damage;
    }
}

public class Hero : MonoBehaviour
{
    public HeroBattleData heroData;
    public bool canMove;
    private TextHp textHp;
    private TextShield textShield;
    public HeroEXP exp { get; private set; }

    private void Awake()
    {
        heroData = new HeroBattleData();
        textHp = GetComponentInChildren<TextHp>();
        textShield = GetComponentInChildren<TextShield>();
        exp = GetComponentInChildren<HeroEXP>();
        heroData.hp = 10;
        heroData.maxHp = 10;
        heroData.shield = 0;
        heroData.maxShield = 10;
        canMove = true;
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
            heroData.hp -= (int)(data.damage - heroData.shield);
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
            heroData.shield -= (int)data.damage;
        }
        textHp.SetHP(heroData);
        textShield.SetShield(heroData);
        dead = false;
        EffectManager.Instance.Hit(transform);
    }

    public void SetHP(int hp)
    {
        heroData.hp = hp;
        if (heroData.hp < 0)
        {
            heroData.hp = 0;
            Die();
        }
        textHp.SetHP(heroData);
    }

    public void Heal(DamageData data)
    {
        if (HealIsDisable()) return;
        heroData.hp += (int)data.damage;
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

    public bool HealIsDisable()
    {
        foreach (var item in CardManager.Instance.cards)
        {
            if(item.TryGetComponent<Monster>(out var monster))
            {
                foreach (var tag in monster.tags)
                {
                    if (tag.type == TagType.NoHope) return true;
                }
            }
        }
        return false;
    }

    public void AddShield(float amount)
    {
        heroData.shield += amount;
        if (heroData.shield > heroData.maxShield)
        {
            heroData.shield = heroData.maxShield;
        }
        textShield.SetShield(heroData);
    }
    public void UpdateDisplay()
    {
        textHp.SetHP(heroData);
        textShield.SetShield(heroData);
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