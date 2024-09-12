using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using Dislands;
using System;

public enum CardSide
{
    Back, Front
}

public enum CardUse
{
    OneTime, ManyTimeUntilDisappear
}

public class Card : MonoBehaviour, IPointerDownHandler
{
    public const bool DEBUG_POSITION = false;
    public const float SPAWN_SPAW_X = -4f;
    public const float SPAWN_SPAW_Y = 8f;
    public const float FLIP_ANIMATION_TIME = 0.3f;
    public const float CARD_MOVE_SPEED = 0.6f;
    public const float CARD_FADE_SPEED = 0.4f;
    public const float SPAWN_DELAY_FLIP = 0.2f;
    public const float DOUBLE_CLICK_PREVENT_TIME = 0.5f;

    private Vector2Int pos;
    [SerializeField] Image cardBack;
    [SerializeField] Transform cardParent;
    public Image icon;
    public TextMeshProUGUI txtName;
    public CardSide side = CardSide.Back;
    public CardUse cardUse;
    [field:SerializeField] public CardData data { get; private set; }
    public CardEffect cardEffect { get; private set; }
    public Action onCardAppear;
    public Action onCardDisappear;
    private CanvasGroup canvasGroup;
    private List<CardEffect> effects = new List<CardEffect>();
    [SerializeField] private bool flipping;
    private float lastClick;

    public Vector2Int Pos
    {
        get { return pos; }
        set
        {
            pos = value;
            GridManager.Instance.dicGrids[pos].card = this;
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        cardEffect = GetComponent<CardEffect>();
    }

    private void OnEnable()
    {
        lastClick = Time.time;
    }


    public void SetData(CardData cardData)
    {
        this.data = cardData;
        if (cardEffect != null && (cardEffect is Monster || cardEffect is Item))
        {
            this.icon.sprite = data.sprite;
            if (this.txtName != null) this.txtName.text = data.name;
        }
    }
    public void ShowSpawnAnimation(float delayFlip = SPAWN_DELAY_FLIP)
    {
        CardManager.Instance.canClick = false;
        side = CardSide.Back;
        cardBack.gameObject.SetActive(true);
        Vector2 startPos = transform.position;
        transform.position = transform.position + new Vector3(SPAWN_SPAW_X, SPAWN_SPAW_Y);
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(startPos, 0.8f));
        sequence.AppendInterval(delayFlip);
        sequence.AppendCallback(() =>
        {
            if (CardManager.Instance.gameMode == GameMode.Normal)
            {
                FlipToFront();
            }
            onCardAppear?.Invoke();
            CardManager.Instance.canClick = true;
        });
    }

    public void FlipToFront()
    {
        if (side == CardSide.Front) return;
        flipping = true;
        side = CardSide.Front;
        cardBack.gameObject.SetActive(true);
        transform.DOScaleX(0f, FLIP_ANIMATION_TIME).OnComplete(() =>
        {
            transform.DOScaleX(CardManager.Instance.DEFAULT_SCALE.x, FLIP_ANIMATION_TIME).OnComplete(() => flipping = false);
            cardBack.gameObject.SetActive(false);
        });
    }

    public void FlipToBack()
    {
        if (side == CardSide.Back) return;
        flipping = true;
        side = CardSide.Back;
        cardBack.gameObject.SetActive(false);
        transform.DOScaleX(0f, FLIP_ANIMATION_TIME).OnComplete(() =>
        {
            transform.DOScaleX(CardManager.Instance.DEFAULT_SCALE.x, FLIP_ANIMATION_TIME).OnComplete(() => flipping = false);
            cardBack.gameObject.SetActive(true);
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Gameplay.Instance.state != GameplayState.Playing) return;
        if (!CardManager.Instance.canClick) return;
        if (flipping) return;
        if (Time.time - lastClick < DOUBLE_CLICK_PREVENT_TIME) return;
        Debug.Log(gameObject.name);
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            lastClick = Time.time;
            Hero hero = CardManager.Instance.hero;
            if (!hero.canMove) return;
            hero = GetComponent<Hero>();
            if (hero != null)
            {
                Debug.Log("You click hero card");
            }
            if (CardManager.Instance.IsNextToHeroCard(this))
            {
                CardManager.Instance.UseCard(this);
            }
        }
        else
        {
            if (cardEffect is Monster)
            {
                Gameplay.Instance.popupInfo.ShowCardMonster(this, (Monster)cardEffect, side);
            }
            else if (cardEffect is Item || cardEffect is Skill)
            {
                Gameplay.Instance.popupInfo.ShowCardItem(this, side);
            }
            else Gameplay.Instance.popupInfo.ShowCardHero(CardManager.selectedHero);
        }
    }

    public void Disappear()
    {
        CardManager.Instance.RemoveCard(this);
        Bounce();
        canvasGroup.DOFade(0f, CARD_FADE_SPEED).OnComplete(() =>
        {
            onCardDisappear?.Invoke();
            EventManager.Instance.onCardDisappeared?.Invoke(this);
            gameObject.SetActive(false);
            if (GridManager.Instance.dicGrids[pos].card == this)
            {
                GridManager.Instance.dicGrids[pos].card = null;
            }
        });
    }
    private void Bounce()
    {
        transform.localScale = Vector3.one;
        EasyEffect.Bounce(gameObject, 0.1f, strength: 0.1f);
    }
    public void MoveToPos(Vector2Int pos)
    {
        this.pos = pos;
        GridManager.Instance.dicGrids[pos].card = this;
        transform.DOMove(GridManager.Instance.dicGrids[pos].transform.position, CARD_MOVE_SPEED);
    }
}