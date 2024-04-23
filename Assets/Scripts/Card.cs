using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public const float SPAWN_SPAW_X = -4f;
    public const float SPAWN_SPAW_Y = 8f;
    public const float FLIP_ANIMATION_TIME = 0.3f;

    public Vector2Int gridPosition;
    [SerializeField] Image icon;
    [SerializeField] Image cardBack;
    [SerializeField] TextMeshProUGUI txtDebug;
    private CardData data;
    public void SetData(CardData cardData)
    {
        this.data = cardData;
        icon.sprite = cardData.sprite;
    }
    public void ShowSpawnAnimation(GridPos grid)
    {

        transform.position = grid.transform.position + new Vector3(SPAWN_SPAW_X, SPAWN_SPAW_Y);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(grid.transform.position, 0.8f));
        sequence.AppendInterval(1f);
        sequence.AppendCallback(() =>
        {
            FlipToFront();
        });
    }

    public void FlipToFront()
    {
        cardBack.gameObject.SetActive(true);
        transform.DOScaleX(0f, FLIP_ANIMATION_TIME).OnComplete(() =>
        {
            transform.DOScaleX(1f, FLIP_ANIMATION_TIME);
            cardBack.gameObject.SetActive(false);
        });
    }

    public void FlipToBack()
    {
        cardBack.gameObject.SetActive(false);
        transform.DOScaleX(0f, FLIP_ANIMATION_TIME).OnComplete(() =>
        {
            transform.DOScaleX(1f, FLIP_ANIMATION_TIME);
            cardBack.gameObject.SetActive(true);
        });
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