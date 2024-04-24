using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform cardParent;
    [SerializeField] private Card heroCard;
    public List<Card> cards { get; private set; }
    private List<Vector2Int> neighbours = new List<Vector2Int>();

    [SerializeField] private GameObject test;
    [SerializeField] private List<GameObject> testPositions = new List<GameObject>();
    private readonly List<Vector2Int> fourDirections = new List<Vector2Int>()
    {
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
        new Vector2Int(1,0),
        new Vector2Int(-1,0)
    };
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

    private IEnumerator IESpawnAllCard()
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
        var neighbours = new List<Vector2Int>();
        for (int i = 0; i < fourDirections.Count; i++)
        {
            Vector2Int newPos = pos + fourDirections[i];
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
        heroCard.MoveToPos(card.pos);
        DOTween.Kill(card.transform);
        card.Disappear();
        var newCard = SpawnCard(spawnNewCardPosition);
        newCard.SetData(DataManager.Instance.noneHeroCardDatas.RandomElement());
        newCard.ShowSpawnAnimation(0f);
        neighbours = GetNeightbourPositions(heroCard.pos);
    }

    public Card GetMoveCard(Card card)
    {
        for (int i = testPositions.Count - 1; i >= 0; i--)
        {
            Destroy(testPositions[i].gameObject);
            testPositions.RemoveAt(i);
        }

        Vector2Int direction = card.pos - heroCard.pos;
        GridPos heroGrid = GridManager.Instance.dicGrids[heroCard.pos];
        Vector2Int straightGrid = heroGrid.pos - direction;
        if (GridManager.Instance.IsInsideGrid(straightGrid))
        {
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
            return GridManager.Instance.dicGrids[positions[0]].card;
        }
        if (card.pos.x == heroCard.pos.x)
        {
            //nếu cùng x: đi lên thì dùng ô bên phải, đi xuống thì dùng ô bên trái (trục tọa độ đi xuống)
            if (card.pos.y < heroCard.pos.y)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].x > heroCard.pos.x) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
            //nếu cùng x: đi lên thì dùng ô bên phải, đi xuống thì dùng ô bên trái
            if (card.pos.y > heroCard.pos.y)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].x < heroCard.pos.x) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
        }
        if (card.pos.y == heroCard.pos.y)
        {
            //nếu cùng y: đi phải thì dùng ô bên trên
            if (card.pos.x > heroCard.pos.x)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].y > heroCard.pos.y) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
            //nếu cùng x: đi trái thì dùng ô bên dưới
            if (card.pos.x < heroCard.pos.x)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].y < heroCard.pos.y) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
        }
        return GridManager.Instance.dicGrids[positions.RandomElement()].card;
    }

    public void ShowDebug(List<Vector2Int> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            var obj = Instantiate(test, GridManager.Instance.dicGrids[positions[i]].transform.position, Quaternion.identity);
            testPositions.Add(obj);
        }
    }
}