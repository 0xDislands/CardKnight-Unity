using UnityEngine;

public class ItemHeal : Item
{
    public int healAmount;
    public override void ApplyEffect(Hero hero)
    {
        Debug.Log("use item heal");
        var data = new DamageData();
        data.damage = 1;
        hero.Heal(data);
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