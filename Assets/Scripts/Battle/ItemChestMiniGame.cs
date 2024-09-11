using UnityEngine;
using Dislands;
using System.Collections.Generic;
using DG.Tweening;

public class ItemChestMiniGame : Item
{
    public ParticleSystem appearEffect;
    public override void ApplyEffect(Hero hero)
    {
        CardManager.Instance.hero.hasMove = false;
        var card = GetComponent<Card>();
        Gameplay.Instance.popupUnlockMiniGame.ShowMiniGame(card, GetRandomCard());
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
        } else if (rand < ratio2)
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