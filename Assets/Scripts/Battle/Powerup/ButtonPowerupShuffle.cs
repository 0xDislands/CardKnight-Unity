﻿using Dislands;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerupShuffle : ButtonPowerup
{
    public override void OnClick()
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return;
        if (!useable) return;
        CurrentAtkTime = 0;
        var cards = CardManager.Instance.cards;
        List<Vector2Int> positions = new List<Vector2Int>();
        var listPos = new List<Vector2Int>();
        int heroIndex = cards.IndexOf(CardManager.Instance.heroCard);
        for (int i = 0; i < cards.Count; i++)
        {
            listPos.Add(cards[i].Pos);
        }
        listPos.Remove(CardManager.Instance.heroCard.Pos);
        listPos.Shuffle();
        List<Vector2Int> newPositions = new List<Vector2Int>();
        int index = -1;
        for (int i = 0; i < cards.Count; i++)
        {
            if (i != heroIndex)
            {
                index++;
                newPositions.Add(listPos[index]);
            } else
            {
                newPositions.Add(CardManager.Instance.heroCard.Pos);
            }
        }
        for (int i = 0; i < newPositions.Count; i++)
        {
            cards[i].MoveToPos(newPositions[i]);
        }
    }
}