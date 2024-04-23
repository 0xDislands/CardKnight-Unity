using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public const float SPAWN_SPAW_X = -4f;
    public const float SPAWN_SPAW_Y = 8f;

    public static CardManager Instance;
    public List<Card> cards;
    public Card cardPrefab;
    public Transform cardParent;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return IESpawnAllCard();
    }

    public IEnumerator IESpawnAllCard()
    {
        cards = new List<Card>();
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            var card = SpawnCard(GridManager.Instance.grids[i]);
            cards.Add(card);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Card SpawnCard(GridPos grid)
    {
        var card = Instantiate(cardPrefab, cardParent);
        card.transform.position = grid.transform.position + new Vector3(SPAWN_SPAW_X, SPAWN_SPAW_Y);
        card.transform.DOMove(grid.transform.position, 0.8f).SetEase(Ease.OutCubic);
        return card;
    }
}