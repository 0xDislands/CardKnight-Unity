using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonSelectHero : MonoBehaviour
{
    public HeroId heroId;
    public Image imgHero;
    public TextMeshProUGUI txtHeroName;
    [SerializeField] private PopupSelectHero popupSelectHero;
    [SerializeField] private bool selectOnEnable;

    private void Start()
    {
        Show(heroId);
    }
    private void OnEnable()
    {
        if (selectOnEnable) OnClick();
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
        popupSelectHero.highlight.gameObject.SetActive(true);
        popupSelectHero.highlight.transform.position = transform.position;
    }

    public void SelectSkin()
    {
        SelectHeroManager.Instance.popupSkin.Display(heroId);
    }
}
