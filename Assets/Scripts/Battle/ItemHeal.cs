using UnityEngine;
using DG.Tweening;

public class ItemHeal : Item
{
    public int healAmount;
    public ParticleSystem healEffect;
    public override void ApplyEffect(Hero hero)
    {
        int rand = Random.Range(1, 6);
        int healValue = Mathf.CeilToInt((rand / 10f) * hero.heroData.maxHp);
        var data = new DamageData();
        data.damage = healValue;
        hero.Heal(data);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(healEffect, hero.transform.position);
        effect.transform.SetParent(hero.transform);
        effect.transform.DOLocalMove(effect.transform.localPosition + new Vector3(0, 150f), 1f);
        var card = GetComponent<Card>();
        card.Disappear();
        CardManager.Instance.MoveCardsAfterUse(card);
    }
}