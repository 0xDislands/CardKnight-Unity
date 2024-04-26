using UnityEngine;
using TMPro;

[System.Serializable]
public class HeroData
{
    public int hp;
    public int shield;
    public int startHp;
    public int dame;
    public int level;
}

[System.Serializable]
public class DamageData
{
    public int damage;
}

public class Hero : MonoBehaviour
{
    public HeroData heroData;
    private TextHp textHp;
    private TextShield textShield;

    private void Awake()
    {
        heroData = new HeroData();
        textHp = GetComponentInChildren<TextHp>();
        textShield = GetComponentInChildren<TextShield>();
        heroData.hp = 10;
        heroData.shield = 0;
    }
    private void Start()
    {
        textHp.SetHP(heroData.hp);
        textShield.SetHP(heroData.shield);
    }

    public void TakeDamage(DamageData data)
    {
        if (data.damage > heroData.shield)
        {
            heroData.hp -= (data.damage - heroData.shield);
            heroData.shield = 0;
            if (heroData.hp < 0)
            {
                Debug.Log("Die!");
                heroData.hp = 0;
            }
        }
        else
        {
            heroData.shield -= data.damage;
        }
        textHp.SetHP(heroData.hp);
        textShield.SetHP(heroData.shield);
    }

    public void Heal(DamageData data)
    {
        heroData.hp += data.damage;
        if (heroData.hp < 0)
        {
            heroData.hp = 0;
        }
        textHp.SetHP(heroData.hp);
    }

    public void AddShield(int amount)
    {
        heroData.shield += amount;
        textShield.SetHP(heroData.shield);
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