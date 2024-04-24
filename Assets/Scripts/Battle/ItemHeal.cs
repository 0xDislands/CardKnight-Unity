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

public class ItemPoison : Item
{
    public override void UseCard(Hero hero)
    {
        hero.gameObject.AddComponent<PoisonEachTurn>();
    }
}