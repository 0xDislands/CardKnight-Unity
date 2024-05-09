using DG.Tweening;
using System.Linq;
using UnityEngine;

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
    public ButtonPowerup[] buttonPowerups;

    private void Awake()
    {
        Instance = this;
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
        foreach (var powerUp in data.powerUps) 
        {
            var button = buttonPowerups.FirstOrDefault(x => x.id == powerUp);
            if(buttonPowerups != null) button.gameObject.SetActive(true);
        }
    }

    public void Lose()
    {
        if (state == GameplayState.Lose) return;
        state = GameplayState.Lose;
        popupGameOver.gameObject.SetActive(true);
    }
}