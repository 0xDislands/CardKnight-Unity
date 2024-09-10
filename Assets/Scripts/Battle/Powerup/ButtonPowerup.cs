using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] protected Hero hero;
    private Camera mainCam;
    public bool isUsingSkill;
    protected TextMeshPro txtNotify;
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
        hero = CardManager.Instance.hero;
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
        if (coolDownImg.fillAmount > 0) return false;
        return true;
    }
    public bool IsLevelReady()
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return false;
        return true;
    }

    public abstract void OnClick();
    public virtual void CancelSkill()
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
    public void Notify(string text)
    {
        txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
        txtNotify.text = text;
    }
    public bool IsOutOfRange()
    {

        return false;
    }

    protected bool SkillDisable()
    {
        foreach (var item in CardManager.Instance.cards)
        {
            if (item.TryGetComponent<Monster>(out var monster))
            {
                foreach (var tag in monster.tags)
                {
                    if (tag.type == TagType.Silient && tag.gameObject.activeInHierarchy) return true;
                }
            }
        }
        return false;
    }
}