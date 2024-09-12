using UnityEngine;

[System.Serializable]
public struct CardData
{
    public CardId id;
    public string name;
    public string description;
    public Sprite sprite;
    public int health;
    public Card cardPrefab;
}