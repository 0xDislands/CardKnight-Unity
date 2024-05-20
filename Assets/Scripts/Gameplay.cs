﻿using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum GameplayState
{
    Prepare, Playing, Win, Lose
}

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;
    public GameplayState state;
    public PopupLevelUp popupLevelUp;
    public PopupPoweupUnlocked popupPoweupUnlocked;
    public PopupGameOver popupGameOver;
    public PopupLockMinigame popupUnlockMiniGame;
    public PopupEvilBuff popupEvilBuff;
    public ProgressBarHeroEXP heroProgressBarExp;
    public Transform powerupGroupParent;
    public PopupToolTip popupToolTip;
    public PopupInfo popupInfo;
    public Toggle cheatToggle;
    public ButtonPowerup[] buttonPowerups { get; private set; }

    private void Awake()
    {
        Instance = this;
        buttonPowerups = powerupGroupParent.GetComponentsInChildren<ButtonPowerup>(true);
    }

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        popupLevelUp.gameObject.SetActive(false);
        popupPoweupUnlocked.gameObject.SetActive(false);
        popupGameOver.gameObject.SetActive(false);
        var cardManager = CardManager.Instance;
        cardManager.StartGame();
        state = GameplayState.Playing;
        var data = DataManager.Instance.dicHero[CardManager.selectedHero];
        for (int i = 0; i < buttonPowerups.Length; i++)
        {
            bool active = data.powerUps.Contains(buttonPowerups[i].id);
            buttonPowerups[i].gameObject.SetActive(active);
            if (active) buttonPowerups[i].transform.SetAsLastSibling(); //sắp xếp skill đúng thứ tự trái qua phải
        }
    }

    public void Lose()
    {
        if (cheatToggle.isOn) return;
        if (state == GameplayState.Lose) return;
        state = GameplayState.Lose;
        popupGameOver.gameObject.SetActive(true);
    }
    public ButtonPowerup GetButtonPowerUpByID(PowerupId id)
    {
        foreach(var buttonPowerup in buttonPowerups)
        {
            if(buttonPowerup.id == id) return buttonPowerup;
        }
        return null;
    }
}