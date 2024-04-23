using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using DarkcupGames;

public class Card : MonoBehaviour, IPointerDownHandler
{
    public const float SPAWN_SPAW_X = -4f;
    public const float SPAWN_SPAW_Y = 8f;
    public const float FLIP_ANIMATION_TIME = 0.3f;
    public const float CARD_MOVE_SPEED = 0.6f;
    public const float CARD_FADE_SPEED = 0.4f;
    public const float SPAWN_DELAY_FLIP = 0.2f;

    public Vector2Int gridPosition;
    [SerializeField] Image icon;
    [SerializeField] Image cardBack;
    [SerializeField] TextMeshProUGUI txtDebug;
    public CardData data;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetData(CardData cardData)
    {
        this.data = cardData;
        icon.sprite = cardData.sprite;
    }
    public void ShowSpawnAnimation(GridPos grid, float delayFlip = SPAWN_DELAY_FLIP)
    {
        transform.position = grid.transform.position + new Vector3(SPAWN_SPAW_X, SPAWN_SPAW_Y);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(grid.transform.position, 0.8f));
        sequence.AppendInterval(delayFlip);
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardManager.Instance.IsNextToHeroCard(this))
        {
            //Debug.Log("You click next to hero card!");
            //Bounce();
            CardManager.Instance.HandleMove(this);
        }
        var hero = GetComponent<Hero>();
        if (hero != null)
        {
            Debug.Log("You click hero card");
        }
    }

    public void Disappear()
    {
        Bounce();
        canvasGroup.DOFade(0f, CARD_FADE_SPEED).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    private void Bounce()
    {
        transform.localScale = Vector3.one;
        EasyEffect.Bounce(gameObject, 0.1f, strength: 0.1f);
    }
    public void MoveToPos(Vector2Int pos)
    {
        this.gridPosition = pos;
        transform.DOMove(GridManager.Instance.dicGrids[pos].transform.position, CARD_MOVE_SPEED);
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

public class Hero : MonoBehaviour
{

}