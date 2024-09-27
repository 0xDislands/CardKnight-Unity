using DG.Tweening;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameplayState
{
    Prepare, Playing, Win, Lose
}

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;
    public static bool fristPlay = true;
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
    public TimeCounter timeCounter;
    public TextMeshProUGUI scoreTxt;
    public GameObject hand;
    public float lastClickTime;
    public float time;
    public ButtonPowerup[] buttonPowerups { get; private set; }
    [SerializeField] private float score;
    public float Score
    {
        get { return score; }
        set {
            score = value;
            scoreTxt.text = $"Score: {(int)(value)}";
        }
    }

    private void Awake()
    {
        Instance = this;
        buttonPowerups = powerupGroupParent.GetComponentsInChildren<ButtonPowerup>(true);
    }

    private IEnumerator Start()
    {
        StartGame();
        yield return new WaitForSeconds(2f);
        if (fristPlay)
        {
            AppearHand();
            fristPlay = false;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time - lastClickTime > 5f && !hand.activeInHierarchy)
        {
            AppearHand();
            //lastClickTime = Time.time;
        }
    }

    private void AppearHand()
    {
        hand.SetActive(true);
        var heroNeighbours = CardManager.Instance.heroNeighbours;
        var destinations = heroNeighbours.FindAll(neighbour => neighbour.y == CardManager.Instance.heroCard.Pos.y);
        var final = destinations[Random.Range(0, destinations.Count)];
        hand.transform.position = GridManager.Instance.dicGrids[final].transform.position;
    }

    public void StartGame()
    {
        Score = 0;
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
            if (active)
            {
                buttonPowerups[i].transform.SetSiblingIndex(data.powerUps.IndexOf(buttonPowerups[i].id)); //sắp xếp skill đúng thứ tự trái qua phải
            } else
            {
                buttonPowerups[i].transform.SetSiblingIndex(buttonPowerups.Length - 1);
            }
        }
        timeCounter.SetTime();
    }

    public void Lose()
    {
        if (Constants.SHOW_CHEAT_OBJECT && cheatToggle.isOn) return;
        if (state == GameplayState.Lose) return;
        state = GameplayState.Lose;
        popupGameOver.gameObject.SetActive(true);
    }

    public ButtonPowerup GetButtonPowerUpByID(PowerupId id)
    {
        foreach (var buttonPowerup in buttonPowerups)
        {
            if (buttonPowerup.id == id) return buttonPowerup;
        }
        return null;
    }
}