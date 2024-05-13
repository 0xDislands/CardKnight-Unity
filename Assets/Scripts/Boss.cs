using UnityEngine;

public class Boss : MonoBehaviour
{
    private Card card;
    private void Awake()
    {
        card = GetComponent<Card>();
        card.onCardAppear += () =>
        {
            CardManager.Instance.SetMode(GameMode.BossMode);
            LeanTween.delayedCall(1f, () =>
            {
                Debug.Log("card disappear");
                CardManager.Instance.FlipDownAllCards();
            });
        };
        card.onCardDisappear += () =>
        {
            Debug.Log("card disappear");
            CardManager.Instance.EndBossMode();
        };
    }
}