using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInfo : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI skillInfo;
    [SerializeField] private TagInfo tagInfo;
    [SerializeField] private Sprite questionMark;


    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.2f);
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
        transform.DOScale(0f, 0.2f).OnComplete(()=> gameObject.SetActive(false));
    }
}
