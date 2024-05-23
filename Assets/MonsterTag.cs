using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TagType
{
    Growth, NoMagic, Revenge, NoHope
}

public abstract class MonsterTag : TurnEndEffect
{
    public TagType type { get; protected set; }
    protected Card card;
    protected Monster monster;
    public Image img;
    protected virtual void Awake()
    {
        card = GetComponentInParent<Card>();
        monster = (Monster)card.cardEffect;
        img = GetComponent<Image>();
        if (type == TagType.Revenge)
        {
            Debug.Log("aaaa");
        }
        img.sprite = DataManager.Instance.dicTag[type].sprite;
    }
}