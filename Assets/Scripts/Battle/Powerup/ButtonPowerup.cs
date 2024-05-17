using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ButtonPowerup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    public PowerupId id;
    protected float maxTurnLeftToUseSkill;
    [SerializeField] protected Image coolDownImg;
    [SerializeField] protected Image icon;
    protected float turnLeftToUSeSkill;
    protected Hero hero;
    private Camera mainCam;
    public bool isUsingSkill;

    public float TurnLeftToUSeSkill
    {
        get { return turnLeftToUSeSkill; }
        set {
            turnLeftToUSeSkill = value;
            coolDownImg.DOFillAmount(turnLeftToUSeSkill / maxTurnLeftToUseSkill, 0.2f);
        }
    }

    private void Awake()
    {
        mainCam = Camera.main;
        hero = CardManager.Instance.hero;
        var data = DataManager.Instance.dicPowerUp[id];
        maxTurnLeftToUseSkill = data.cooldown;
        icon.sprite = data.sprite;
        var text = GetComponentInChildren<TextPowerupName>();
        if (text)
        {
            text.id = id;
            text.UpdateDisplay();
        }
    }

    private void OnEnable()
    {
        turnLeftToUSeSkill = 0;
        if (!IsUnlocked()) coolDownImg.fillAmount = 1f;
    }

    public bool IsUnlocked()
    {
        var hero = CardManager.Instance.hero;
        var powerData = DataManager.Instance.dicPowerUp[id];
        return hero.heroData.level >= powerData.unlockLevel;
    }

    public bool IsCooldownReady()
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return false; //chuoi
        if (coolDownImg.fillAmount > 0) return false;
        return true;
    }

    public abstract void OnClick();
    public virtual void ResetSkill()
    {
        isUsingSkill = false;
        TurnLeftToUSeSkill = 0;
        hero.canMove = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 1.2f, 0.2f);
        Gameplay.Instance.popupToolTip.DisplayToolTip(DataManager.Instance.dicPowerUp[id].description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f);
        Gameplay.Instance.popupToolTip.HideToolTip();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) Gameplay.Instance.popupInfo.DisplaySkill(id);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        var pos = mainCam.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        Gameplay.Instance.popupToolTip.SetPosition(pos);
    }
}