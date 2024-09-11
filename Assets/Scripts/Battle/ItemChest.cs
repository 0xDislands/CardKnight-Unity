using UnityEngine;
using Dislands;
using System.Collections.Generic;

public class ItemChest : Item
{
    public ParticleSystem appearEffect;
    public override void ApplyEffect(Hero hero)
    {
        CardManager.Instance.hero.hasMove = false;
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(appearEffect, transform.position);
        var card = GetComponent<Card>();
        Debug.Log("calling spawn card item chest");
        var newCard = CardManager.Instance.SpawnCard(card.Pos, GetRandomCard());
        newCard.MoveToPos(card.Pos);
        card.Disappear();
    }

    public CardId GetRandomCard()
    {
        int ratio1 = 5;
        int ratio2 = 30 + ratio1;
        int ratio3 = 10 + ratio2;
        int ratio4 = 55 + ratio3;

        int rand = Random.Range(0, 101);
        if (rand < ratio1)
        {
            return CardId.SkillFire;
        } 
        else if (rand < ratio2)
        {
            return CardId.ItemShield;
        } else if (rand < ratio3)
        {
            return CardId.ItemPoison;
        } else
        {
            return CardId.ItemHeal;
        }
    }
}