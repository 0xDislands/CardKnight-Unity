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
    [SerializeField] private TextMeshProUGUI skillInfo;
    [SerializeField] private TagInfo tagInfo;
    [SerializeField] private Sprite questionMark;
    [SerializeField] private Transform body;

    private void OnEnable()
    {
        body.transform.localScale = Vector3.zero;
        body.transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);
        skillInfo.text = "";
    }

    public void DisplaySkill(PowerupId id)
    {
        gameObject.SetActive(true);
        var data = DataManager.Instance.dicPowerUp[id];
        icon.sprite = data.sprite;
        icon.preserveAspect = true;
        skillInfo.gameObject.SetActive(true);
        tagInfo.gameObject.SetActive(false);
        skillInfo.text = data.description;
    }

    public void DisplayCard(HeroId id)
    {
        gameObject.SetActive(true);
        var data = DataManager.Instance.dicHero[id];
        icon.sprite = data.sprite;
        icon.preserveAspect = true;
        skillInfo.gameObject.SetActive(true);
        tagInfo.gameObject.SetActive(false);
        skillInfo.text = data.description;
    }


    public void DisplayCard(Sprite icon, MonsterTag[] tags, CardSide side)
    {
        gameObject.SetActive(true);
        if (side == CardSide.Back) this.icon.sprite = questionMark;
        else this.icon.sprite = icon;
        this.icon.preserveAspect = true;
        skillInfo.gameObject.SetActive(false);
        tagInfo.gameObject.SetActive(true);
        tagInfo.DisplayTags(tags, side);
    }

    public void DisplayCard(Card card, CardSide side)
    {
        gameObject.SetActive(true);
        if (side == CardSide.Back) this.icon.sprite = questionMark;
        else icon.sprite = card.icon.sprite;

        icon.preserveAspect = true;
        skillInfo.gameObject.SetActive(true);
        tagInfo.gameObject.SetActive(false);
        skillInfo.text = "";
    }

    public void Close()
    {
        body.transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(()=> gameObject.SetActive(false));
    }
}
