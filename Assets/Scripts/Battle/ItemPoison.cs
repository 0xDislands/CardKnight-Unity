﻿using UnityEngine;

public class ItemPoison : Item
{
    public ParticleSystem poisonEffect;
    public override void ApplyEffect(Hero hero)
    {
        PoisonEachTurn poison;
        if(hero.TryGetComponent<PoisonEachTurn>(out poison))
        {
            poison.poisonEffect = poisonEffect;
            poison.ResetTurnCount();
        } 
        else
        {
            poison = hero.gameObject.AddComponent<PoisonEachTurn>();
            poison.poisonEffect = poisonEffect;
        }

        var effect = SimpleObjectPool.Instance.GetObjectFromPool(poisonEffect, hero.transform.position);
        effect.transform.SetParent(hero.transform);

        var card = GetComponent<Card>();
        card.Disappear();
        CardManager.Instance.MoveCardsAfterUse(card);
    }
}