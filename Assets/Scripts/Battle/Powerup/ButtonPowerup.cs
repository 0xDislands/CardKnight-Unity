using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonPowerup : MonoBehaviour
{
    public PowerupId id;
    protected float atkToAvailable;
    [SerializeField] protected Image coolDownImg;
    [SerializeField] protected Image icon;
    protected float currentAtkTime;
    protected bool useable;

    public float CurrentAtkTime
    {
        get { return currentAtkTime; }
        set {
            currentAtkTime = value;
            coolDownImg.DOFillAmount(currentAtkTime / atkToAvailable, 0.2f).OnComplete(() => 
            {
                useable = coolDownImg.fillAmount >= 1f;
            });
        }
    }

    private void Awake()
    {
        var data = DataManager.Instance.dicPowerUp[id];
        atkToAvailable = data.cooldown;
        icon.sprite = data.sprite;
    }

    private void OnEnable()
    {
        CurrentAtkTime = atkToAvailable;
        useable = coolDownImg.fillAmount >= 1f;
    }

    public bool IsUnlocked()
    {
        var hero = CardManager.Instance.hero;
        var powerData = DataManager.Instance.dicPowerUp[id];
        return hero.heroData.level >= powerData.unlockLevel;
    }

    public abstract void OnClick();
}

//public class ButtonPowerupCooldown : MonoBehaviour
//{
//    public float maxValue;
//    public float currentValue;
//    private Image coolDownImg;

//    private void Awake()
//    {
//        coolDownImg = GetComponent<Image>();
//    }
//}