using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterTag : TurnEndEffect
{
    protected Card card;
    protected Monster monster;
    protected virtual void Awake()
    {
        card = GetComponent<Card>();
        monster = (Monster)card.cardEffect;
    }
}
