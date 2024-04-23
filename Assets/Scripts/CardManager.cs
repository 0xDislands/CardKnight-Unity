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
    public Card heroCard;
    private List<Vector2Int> neighbours = new List<Vector2Int>();

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return IESpawnAllCard();
        UpdateNeighbourPos();
    }

    private void Update()
    {
        for (int i = 0; i < neighbours.Count; i++)
        {
            Debug.DrawLine(heroCard.transform.position, GridManager.Instance.dicGrids[neighbours[i]].transform.position, Color.red);
        }
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
                card.gameObject.AddComponent<Hero>();
                heroCard = card;
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

    public bool IsNextToHeroCard(Card card)
    {
        return neighbours.Contains(card.gridPosition);
    }

    public void UpdateNeighbourPos()
    {
        List<Vector2Int> positions = new List<Vector2Int>()
        {
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
            new Vector2Int(1,0),
            new Vector2Int(-1,0)
        };
        neighbours = new List<Vector2Int>();
        for (int i = 0; i < positions.Count; i++)
        {
            Vector2Int newPos = heroCard.gridPosition + positions[i];
            if (GridManager.Instance.dicGrids.ContainsKey(newPos))
            {
                neighbours.Add(newPos);
            }
        }
    }

    public void HandleMove(Card card)
    {
        var oldHeroPos = heroCard.gridPosition;
        var oldGrid = GridManager.Instance.dicGrids[oldHeroPos];
        var newCard = SpawnCard(oldGrid);
        newCard.SetData(DataManager.Instance.noneHeroCardDatas.RandomElement());
        newCard.ShowSpawnAnimation(oldGrid, 0f);

        heroCard.MoveToPos(card.gridPosition);
        card.Disappear();
        UpdateNeighbourPos();
    }
}