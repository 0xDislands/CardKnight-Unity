using DG.Tweening;
using Dislands;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInfo : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;

    [SerializeField] private Image icon;
    [SerializeField] private Image cardBackground;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TagInfo tagInfo;
    [SerializeField] private Sprite questionMark;
    [SerializeField] private Transform body;

    private void OnEnable()
    {
        body.transform.localScale = Vector3.zero;
        body.transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);
        txtDescription.text = "";
    }

    public void DisplaySkill(PowerupId id)
    {
        gameObject.SetActive(true);
        cardBackground.color = Color.white;
        var data = DataManager.Instance.dicPowerUp[id];
        icon.sprite = data.sprite;
        icon.preserveAspect = true;
        txtDescription.gameObject.SetActive(true);
        tagInfo.gameObject.SetActive(false);
        txtDescription.text = data.description;
        txtName.text = data.name;
    }

    public void ShowCardHero(HeroId id)
    {
        gameObject.SetActive(true);
        cardBackground.color = Color.white;
        var data = DataManager.Instance.dicHero[id];
        icon.sprite = data.sprite;
        icon.preserveAspect = true;
        txtDescription.gameObject.SetActive(true);
        tagInfo.gameObject.SetActive(false);
        txtDescription.text = data.name;
        txtName.text = "Hero";
    }


    public void ShowCardMonster(Card card, Monster monster, CardSide side)
    {
        gameObject.SetActive(true);
        cardBackground.color = Color.white;
        if (side == CardSide.Back)
        {
            this.icon.sprite = questionMark;
            txtName.text = "Unknown";
            txtDescription.text = "Unknown";
            cardBackground.color = Color.black;
        } 
        else
        {
            this.icon.sprite = card.icon.sprite;
            txtName.text = card.data.name;
            txtDescription.text = card.data.description;
            cardBackground.color = Color.white;
        }
        this.icon.preserveAspect = true;
        txtDescription.gameObject.SetActive(false);
        tagInfo.gameObject.SetActive(true);
        tagInfo.DisplayTags(monster.tags, side);
    }

    public void ShowCardItem(Card card, CardSide side)
    {
        gameObject.SetActive(true);
        cardBackground.color = Color.white;
        if (side == CardSide.Back)
        {
            this.icon.sprite = questionMark;
            txtName.text = "Unknown";
            txtDescription.text = "Unknown";
            cardBackground.color = Color.black;
        }
        else
        {
            icon.sprite = card.icon.sprite;
            txtDescription.text = card.data.description;
            txtName.text = card.data.name;
            cardBackground.color = Color.white;
        }
        icon.preserveAspect = true;
        txtDescription.gameObject.SetActive(true);
        tagInfo.gameObject.SetActive(false);
    }

    public void Close()
    {
        body.transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(()=> gameObject.SetActive(false));
    }
}
