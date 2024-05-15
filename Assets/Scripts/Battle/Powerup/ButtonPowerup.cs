using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ButtonPowerup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public PowerupId id;
    protected float atkToAvailable;
    [SerializeField] protected Image coolDownImg;
    [SerializeField] protected Image icon;
    protected float currentAtkTime;
    protected bool fullCoolDown;
    protected Hero hero;
    private Camera mainCam;
    protected bool active;

    public float CurrentAtkTime
    {
        get { return currentAtkTime; }
        set {
            currentAtkTime = value;
            coolDownImg.DOFillAmount(currentAtkTime / atkToAvailable, 0.2f).OnComplete(() => 
            {
                fullCoolDown = coolDownImg.fillAmount <= 0f;
            });
        }
    }

    private void Awake()
    {
        mainCam = Camera.main;
        hero = CardManager.Instance.hero;
        var data = DataManager.Instance.dicPowerUp[id];
        atkToAvailable = data.cooldown;
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
        currentAtkTime = 0;
        if (!IsUnlocked()) coolDownImg.fillAmount = 1f;
    }

    public bool IsUnlocked()
    {
        var hero = CardManager.Instance.hero;
        var powerData = DataManager.Instance.dicPowerUp[id];
        return hero.heroData.level >= powerData.unlockLevel;
    }

    public bool IsCooldownReady(bool showEffect = true)
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return false;
        if (!fullCoolDown)
        {
            if (showEffect) SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0,1f));
            return false;
        }
        return true;
    }

    public abstract void OnClick();
    public virtual void ResetSkill()
    {
        active = false;
        CurrentAtkTime = 0;
        hero.canMove = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * 1.2f, 0.2f);

        var pos = mainCam.ScreenToWorldPoint(eventData.position);
        pos.z = 0;
        Gameplay.Instance.popupToolTip.DisplayToolTip(DataManager.Instance.dicPowerUp[id].description, pos) ;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one, 0.2f);
        Gameplay.Instance.popupToolTip.HideToolTip();
    }
}