using Dislands;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerupShuffle : ButtonPowerup
{
    public override void OnClick()
    {
        if (IsCooldownReady() == false) return;
        CurrentAtkTime = atkToAvailable;
        var cards = CardManager.Instance.cards;
        List<Vector2Int> positions = new List<Vector2Int>();
        var listPos = new List<Vector2Int>();
        int heroIndex = cards.IndexOf(CardManager.Instance.heroCard);
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].GetComponentInChildren<ImueMagicTag>() != null) continue;
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

        string before = "";
        for (int i = 0; i < cards.Count; i++)
        {
            before += cards[i].Pos + " ";
        }
        string after = "";
        for (int i = 0; i < cards.Count; i++)
        {
            after += newPositions[i] + " ";
        }
        Debug.Log("prepare to swap");
        Debug.Log("before: " + before);
        Debug.Log("after: " + after);

        for (int i = 0; i < newPositions.Count; i++)
        {
            cards[i].MoveToPos(newPositions[i]);
        }
    }

    public override void ResetSkill()
    {
    }
}