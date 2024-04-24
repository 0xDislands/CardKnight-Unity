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
    public GameObject test;
    public List<GameObject> testPositions = new List<GameObject>();

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
            var card = SpawnCard(GridManager.Instance.grids[i].pos);
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
            card.name = "Card" + i;
            cards.Add(card);
            card.ShowSpawnAnimation();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Card SpawnCard(Vector2Int pos)
    {
        var grid = GridManager.Instance.dicGrids[pos];
        var card = Instantiate(cardPrefab, cardParent);
        card.pos = grid.pos;
        card.transform.position = grid.transform.position;
        card.gameObject.name = "Card #" + Random.Range(100, 200); 
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
        var moveCard = GetMoveCard(card);
        var spawnNewCardPosition = moveCard.pos;
        moveCard.MoveToPos(heroCard.pos);
        Debug.Log($"move {moveCard.gameObject.name} movecard to position : " + moveCard.pos);
        heroCard.MoveToPos(card.pos);
        DOTween.Kill(card.transform);
        card.Disappear();

        Debug.Log("spawn new card at position : " + moveCard.pos);
        var newCard = SpawnCard(spawnNewCardPosition);
        newCard.SetData(DataManager.Instance.noneHeroCardDatas.RandomElement());
        newCard.ShowSpawnAnimation(0f);

        neighbours = GetNeightbourPositions(heroCard.pos);
    }

    public Card GetMoveCard(Card card)
    {
        Debug.Log("Find move card!");
        Vector2Int direction = card.pos - heroCard.pos;
        GridPos heroGrid = GridManager.Instance.dicGrids[heroCard.pos];
        Vector2Int straightGrid = heroGrid.pos - direction;
        if (GridManager.Instance.IsInsideGrid(straightGrid))
        {
            Debug.Log("straight card valid, return straight card");
            return GridManager.Instance.dicGrids[straightGrid].card;
        }
        var positions = GetNeightbourPositions(heroGrid.pos);
        for (int i = positions.Count - 1; i >= 0; i--)
        {
            if (positions[i] == card.pos) positions.RemoveAt(i);
        }
        ShowDebug(positions);
        if (positions.Count == 1)
        {
            Debug.Log("position available is 1, return only 1");
            Debug.Log("return move card: " + positions[0]);
            return GridManager.Instance.dicGrids[positions[0]].card;
        }
        Debug.Log("return random card");
        return GridManager.Instance.dicGrids[positions.RandomElement()].card;
    }

    public void ShowDebug(List<Vector2Int> positions)
    {
        for (int i = testPositions.Count - 1; i >= 0; i--)
        {
            Destroy(testPositions[i].gameObject);
            testPositions.RemoveAt(i);
        }
        for (int i = 0; i < positions.Count; i++)
        {
            var t = Instantiate(test, GridManager.Instance.dicGrids[positions[i]].transform.position, Quaternion.identity);
        }
    }
}