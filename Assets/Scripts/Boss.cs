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
                CardManager.Instance.FlipDownAllCards();
            });
        };
        card.onCardDisappear += () =>
        {
            CardManager.Instance.EndBossMode();
        };
    }
}