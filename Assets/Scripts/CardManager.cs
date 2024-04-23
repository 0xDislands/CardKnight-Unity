using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public Sprite cardBack;
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
        int midIndex = GridManager.Instance.grids.Length / 2;
        cards = new List<Card>();
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            var card = SpawnCard(GridManager.Instance.grids[i]);
            if (i == midIndex)
            {
                card.SetData(DataManager.Instance.dicCardDatas[CardId.Hero]);
            } else
            {
                card.SetData(DataManager.Instance.noneHeroCardDatas.RandomElement());
            }
            cards.Add(card);
            card.ShowSpawnAnimation(GridManager.Instance.grids[i]);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Card SpawnCard(GridPos grid)
    {
        var card = Instantiate(cardPrefab, cardParent);
        card.gridPosition = grid.gridPosition;
        grid.card = card;
        return card;
    }
}