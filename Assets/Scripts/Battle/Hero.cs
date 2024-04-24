using UnityEngine;
using TMPro;

public class HeroData
{
    public int hp;
    public int startHp;
    public int dame;
}

public class DamageData
{
    public int damage;
}

public class Hero : MonoBehaviour, IHealthReadable
{
    public HeroData heroData;
    private TextHp textHp;

    public void TakeDamage(DamageData damage)
    {
        heroData.hp -= damage.damage;
        if (heroData.hp < 0)
        {
            heroData.hp = 0;
        }
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