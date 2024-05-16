using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tittle;
    [SerializeField] private Image[] avatars;
    private HeroId displayId;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1f, 0.2f);
    }

    public void Display(HeroId id)
    {
        displayId = id;
        gameObject.SetActive(true);
        var data = DataManager.Instance.dicHero[id];
        tittle.text = $"Select {id}'s skin";
        for (int i = 0; i < avatars.Length; i++)
        {
            avatars[i].sprite = data.skins[i];
            avatars[i].preserveAspect = true;
        }
    }

    public void ChooseSkin(int index)
    {
        var data = DataManager.Instance.dicHero[displayId];
        data.selectSkin = index;
        var button = SelectHeroManager.Instance.popupSelectHero.buttonSelectHeros.First(x => x.heroId == displayId);
        button.imgHero.sprite = data.skins[index];
        button.imgHero.preserveAspect = true;
        Close();
    }

    public void Close()
    {
        transform.DOScale(0f, 0.1f).OnComplete(() => 
        {
            gameObject.SetActive(false);
        });
    }
}
