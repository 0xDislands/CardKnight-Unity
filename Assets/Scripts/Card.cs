using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public Vector2Int gridPosition;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI txtDebug;
    private CardData data;
    public void SetData(CardData cardData)
    {
        this.data = cardData;
        icon.sprite = cardData.sprite;
    }
}

[System.Serializable]
public struct CardData
{
    public CardId id;
    public Sprite sprite;
    public int health;
}

public enum CardId
{
    None,
    Hero,
    Potion,
    Enemy,
    Chest,
    Gold,
    Diamond,
    Random
}