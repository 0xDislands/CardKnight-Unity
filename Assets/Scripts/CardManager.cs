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
        neighbours = GetNeightbourPositions(heroCard.pos);
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
                heroCard.pos = GridManager.Instance.grids[i].pos;
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
        card.pos = grid.pos;
        grid.card = card;
        return card;
    }

    public bool IsNextToHeroCard(Card card)
    {
        return neighbours.Contains(card.pos);
    }

    public List<Vector2Int> GetNeightbourPositions(Vector2Int pos)
    {
        List<Vector2Int> positions = new List<Vector2Int>()
        {
            new Vector2Int(0,1),
            new Vector2Int(0,-1),
            new Vector2Int(1,0),
            new Vector2Int(-1,0)
        };
        var neighbours = new List<Vector2Int>();
        for (int i = 0; i < positions.Count; i++)
        {
            Vector2Int newPos = pos + positions[i];
            if (GridManager.Instance.dicGrids.ContainsKey(newPos))
            {
                neighbours.Add(newPos);
            }
        }
        return neighbours;
    }

    public void HandleMove(Card card)
    {
        Vector2Int direction = card.pos - heroCard.pos;

        GridPos heroGrid = GridManager.Instance.dicGrids[heroCard.pos];

        Vector2Int straightGrid = heroGrid.pos - direction;
        if (GridManager.Instance.IsInsideGrid(straightGrid))
        {
            var grid = GridManager.Instance.dicGrids[straightGrid];
            var cell = grid.card;
            cell.MoveToPos(heroGrid.pos);
            var newCard = SpawnCard(grid);
            newCard.SetData(DataManager.Instance.noneHeroCardDatas.RandomElement());
            newCard.ShowSpawnAnimation(grid, 0f);
        } else
        {
            var newCard = SpawnCard(heroGrid);
            newCard.SetData(DataManager.Instance.noneHeroCardDatas.RandomElement());
            newCard.ShowSpawnAnimation(heroGrid, 0f);
        }

        heroCard.MoveToPos(card.pos);
        card.Disappear();
        neighbours = GetNeightbourPositions(heroCard.pos);
    }
}