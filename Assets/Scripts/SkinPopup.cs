using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SkinPopup : MonoBehaviour
{
    public const float ANIM_TIME = 0.5f;

    [SerializeField] private TextMeshProUGUI tittle;
    [SerializeField] private Transform body;
    [SerializeField] private Image[] avatars;
    private HeroId displayId;
    private void OnEnable()
    {
        body.transform.localScale = Vector3.zero;
        body.transform.DOScale(1f, ANIM_TIME).SetEase(Ease.OutBack);
    }

    public void Display(HeroId id)
    {
        displayId = id;
        gameObject.SetActive(true);
        var data = DataManager.Instance.dicHero[id];
        tittle.text = $"Select {id}'s skin".ToUpper();
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].sprite = data.skins[i];
            avatars[i].preserveAspect = true;
        }
    }

    public void ChooseSkin(int index)
    {
        var data = DataManager.Instance.dicHero[displayId];
        data.selectedSkin = index;
        var button = SelectHeroManager.Instance.popupSelectHero.buttonSelectHeros.First(x => x.heroId == displayId);
        button.imgHero.sprite = data.skins[index];
        button.imgHero.preserveAspect = true;
        Close();
    }

    public void Close()
    {
        body.transform.DOScale(0f, ANIM_TIME).SetEase(Ease.InBack).OnComplete(() => 
        {
            gameObject.SetActive(false);
        });
    }
}