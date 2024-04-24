using UnityEngine;

public class Item : CardEffect
{
    public override void UseCard(Hero hero)
    {

    }
}

public class ItemHeal : Item
{
    public int healAmount;
    public override void UseCard(Hero hero)
    {
        hero.heroData.hp += healAmount;
        var hp = hero.GetComponentInChildren<TextHp>();
        if (hp) hp.UpdateHP();
    }
}