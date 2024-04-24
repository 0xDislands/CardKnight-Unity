using UnityEngine;

public class Monster : MonoBehaviour, ICardTurn, IHealthReadable
{
    public MonsterData monsterData;

    public int GetHP()
    {
        return monsterData.hp; 
    }

    public void UseCard(Hero hero)
    {
        var damage = new DamageData();
        damage.damage = monsterData.hp;
        hero.TakeDamage(damage);
    }

    private void OnDisable()
    {
        Destroy(this);
    }
}
