using DG.Tweening;
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

    private void Awake()
    {
        Instance = this;
    }

    public void Lose()
    {
        if (state == GameplayState.Lose) return;
        state = GameplayState.Lose;
        DOVirtual.DelayedCall(0.5f, () =>
        {
            popupGameOver.gameObject.SetActive(true);
        });
    }
}