using UnityEngine;
using DarkcupGames;

public class ItemHeal : Item
{
    public int healAmount;
    public ParticleSystem healEffect;
    public override void ApplyEffect(Hero hero)
    {
        Debug.Log("use item heal");
        var data = new DamageData();
        data.damage = 1;
        hero.Heal(data);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(healEffect, hero.transform.position);
        effect.transform.SetParent(hero.transform);
    }
}

public class ItemPoison : Item
{
    public override void ApplyEffect(Hero hero)
    {
        Debug.Log("use item poison");
        hero.gameObject.AddComponent<PoisonEachTurn>();
    }
}