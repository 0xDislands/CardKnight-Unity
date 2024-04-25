using UnityEngine;
using TMPro;

[System.Serializable]
public class HeroData
{
    public int hp;
    public int startHp;
    public int dame;
    public int level = 1;
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

    private void Awake()
    {
        heroData = new HeroData();
        textHp = GetComponentInChildren<TextHp>();
        heroData.hp = 10;
    }
    private void Start()
    {
        textHp.SetHP(heroData.hp);
    }

    public void TakeDamage(DamageData damage)
    {
        heroData.hp -= damage.damage;
        if (heroData.hp < 0)
        {
            heroData.hp = 0;
        }
        textHp.SetHP(heroData.hp);
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

    public void Die()
    {
        Debug.Log("game over!");
    }

    public int GetHP()
    {
        return heroData.hp;
    }
}