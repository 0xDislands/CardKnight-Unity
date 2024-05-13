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
    public int shield;
    public int maxShield;
    public int startHp;
    public int damage;
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
    public HeroBattleData heroData;
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