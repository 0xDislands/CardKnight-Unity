using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public Vector2Int pos;
    [SerializeField] SpriteRenderer icon;
    [SerializeField] Sprite[] sprites;
    [SerializeField] TextMeshProUGUI txtDebug;

    private void Start()
    {
        //icon.sprite = sprites[Random.Range(0, sprites.Length)];
    }
    private void Update()
    {
        
    }
}

[System.Serializable]
public struct CardData
{
    public Sprite sprite;
    public int health;
}