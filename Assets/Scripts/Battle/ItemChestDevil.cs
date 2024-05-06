using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Dislands;

public class ItemChestDevil : Item
{
    public ParticleSystem appearEffect;
    public List<CardId> transformCards = new List<CardId>() { CardId.ItemHeal, CardId.ItemPoison, CardId.ItemShield };
    public override void ApplyEffect(Hero hero)
    {
        var card = GetComponent<Card>();
        Gameplay.Instance.popupEvilBuff.ShowLevelUp(card);
    }
}