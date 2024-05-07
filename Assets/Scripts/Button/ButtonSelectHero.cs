using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectHero : MonoBehaviour
{
    public HeroId heroId;
    public Image imgHero;
    public TextMeshProUGUI txtHeroName;

    private void Start()
    {
        Show(heroId);
    }
    public void Show(HeroId id)
    {
        heroId = id;
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        var data = DataManager.Instance.dicHero[heroId];
        txtHeroName.text = data.name;
        imgHero.sprite = data.sprite;
    }
    public void OnClick()
    {
        SelectHeroManager.Instance.SelectHero(heroId);
    }
}