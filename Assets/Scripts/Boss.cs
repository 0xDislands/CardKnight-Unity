using UnityEngine;

public class Boss : MonoBehaviour
{
    private Card card;
    private void Awake()
    {
        card = GetComponent<Card>();
        card.onCardAppear += () =>
        {
            Debug.Log("card disappear");
            CardManager.Instance.StartBossMode();
        };
        card.onCardDisappear += () =>
        {
            Debug.Log("card disappear");
            CardManager.Instance.EndBossMode();
        };
    }
}