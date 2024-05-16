using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupInfo : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI info;

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
        info.text = data.description;
    }

    public void DisplayCard(HeroId id)
    {
        gameObject.SetActive(true);
        var data = DataManager.Instance.dicHero[id];
        icon.sprite = data.sprite;
        icon.preserveAspect = true;
        info.text = data.description;
    }


    public void DisplayCard(Sprite icon, string info)
    {
        gameObject.SetActive(true);
        this.icon.sprite = icon;
        this.icon.preserveAspect = true;
        this.info.text = info;
    }

    public void Close()
    {
        transform.DOScale(0f, 0.2f).OnComplete(()=> gameObject.SetActive(false));
    }
}
