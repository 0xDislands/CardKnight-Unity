using UnityEngine;

public class ItemShield : Item
{
    public float shieldAmount;
    public ParticleSystem effect;
    public override void ApplyEffect(Hero hero)
    {
        shieldAmount = CardManager.Instance.hero.heroData.maxShield * 0.1f;
        hero.AddShield(shieldAmount);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(this.effect, hero.transform.position);
        effect.transform.SetParent(hero.transform);

        var card = GetComponent<Card>();
        card.Disappear();
        CardManager.Instance.MoveCardsAfterUse(card);
    }
}