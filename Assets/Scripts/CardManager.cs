using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkcupGames;

public class CardManager : MonoBehaviour
{
    public const bool SHOW_DEBUG = false;

    public static CardManager Instance;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform cardParent;
    [SerializeField] private Card heroCard;
    public Hero hero;
    public List<Card> cards { get; private set; }
    private List<Vector2Int> heroNeighbours = new List<Vector2Int>();

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

    private void Start()
    {
        SpawnAllCard();
        heroNeighbours = GetNeightbourPositions(heroCard.Pos);
        StartCoroutine(IECardAnimation());
    }

    private void Update()
    {
        for (int i = 0; i < heroNeighbours.Count; i++)
        {
            Debug.DrawLine(heroCard.transform.position, GridManager.Instance.dicGrids[heroNeighbours[i]].transform.position, Color.red);
        }
    }

    private void SpawnAllCard()
    {
        int midIndex = GridManager.Instance.grids.Length / 2;
        cards = new List<Card>();
        heroCard = SpawnCard(GridManager.Instance.grids[midIndex].pos, CardId.Hero); 
        hero = heroCard.GetComponent<Hero>();
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            Card card;
            CardId id;
            if (i == midIndex)
            {
                card = heroCard;
            } else
            {
                id = DataManager.Instance.noneHeroCardDatas.RandomElement().id;
                card = SpawnCard(GridManager.Instance.grids[i].pos, id);
            }
            card.name = "Card" + i;
            cards.Add(card);
            card.transform.position = new Vector2(999f, 999f);
        }
    }

    IEnumerator IECardAnimation()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.position = GridManager.Instance.dicGrids[cards[i].Pos].transform.position;
            cards[i].ShowSpawnAnimation();
            yield return new WaitForSeconds(0.1f);
        }
    }

    private Card SpawnCard(Vector2Int pos, CardId id)
    {
        var data = DataManager.Instance.dicCardDatas[id];
        var grid = GridManager.Instance.dicGrids[pos];
        var card = Instantiate(data.cardPrefab, cardParent);
        card.Pos = grid.pos;
        card.transform.position = grid.transform.position;
        card.gameObject.name = "Card #" + Random.Range(100, 200); 
        return card;
    }

    public bool IsNextToHeroCard(Card card)
    {
        return heroNeighbours.Contains(card.Pos);
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

    public void MoveCardsAfterUse(Card card)
    {
        var moveCard = GetMoveCard(card);
        var spawnNewCardPosition = moveCard.Pos;
        Debug.Log($"is corner card = {GridManager.Instance.IsCornerCard(heroCard.Pos)}");
        //Nếu ngay góc thì di chuyển toàn bộ cột
        if (GridManager.Instance.IsCornerCard(heroCard.Pos))
        {
            Vector2Int destination = heroCard.Pos;
            Vector2Int direction = heroCard.Pos - moveCard.Pos;
            while (true)
            {
                Vector2Int oldPos = destination - direction;
                if (GridManager.Instance.IsInsideGrid(oldPos) == false)
                {
                    break;
                }
                var oldCard = GridManager.Instance.dicGrids[oldPos].card;
                oldCard.MoveToPos(destination);
                destination = oldPos;
            }
            spawnNewCardPosition = destination;
        } else //Nếu không chỉ move card cần move
        {
            moveCard.MoveToPos(heroCard.Pos);
        }
        heroCard.MoveToPos(card.Pos);
        DOTween.Kill(card.transform);
        card.Disappear();
        var newCard = SpawnCard(spawnNewCardPosition, DataManager.Instance.noneHeroCardDatas.RandomElement().id);
        newCard.ShowSpawnAnimation(0f);
        heroNeighbours = GetNeightbourPositions(heroCard.Pos);
    }

    public Card GetMoveCard(Card card)
    {
        for (int i = testPositions.Count - 1; i >= 0; i--)
        {
            Destroy(testPositions[i].gameObject);
            testPositions.RemoveAt(i);
        }

        Vector2Int direction = card.Pos - heroCard.Pos;
        GridPos heroGrid = GridManager.Instance.dicGrids[heroCard.Pos];
        Vector2Int straightGrid = heroGrid.pos - direction;
        if (GridManager.Instance.IsInsideGrid(straightGrid))
        {
            return GridManager.Instance.dicGrids[straightGrid].card;
        }
        var positions = GetNeightbourPositions(heroGrid.pos);
        for (int i = positions.Count - 1; i >= 0; i--)
        {
            if (positions[i] == card.Pos) positions.RemoveAt(i);
        }
        ShowDebug(positions);
        if (positions.Count == 1)
        {
            return GridManager.Instance.dicGrids[positions[0]].card;
        }
        if (card.Pos.x == heroCard.Pos.x)
        {
            //nếu cùng x: đi lên thì dùng ô bên phải, đi xuống thì dùng ô bên trái (trục tọa độ đi xuống)
            if (card.Pos.y < heroCard.Pos.y)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].x > heroCard.Pos.x) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
            //nếu cùng x: đi lên thì dùng ô bên phải, đi xuống thì dùng ô bên trái
            if (card.Pos.y > heroCard.Pos.y)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].x < heroCard.Pos.x) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
        }
        if (card.Pos.y == heroCard.Pos.y)
        {
            //nếu cùng y: đi phải thì dùng ô bên trên
            if (card.Pos.x > heroCard.Pos.x)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].y > heroCard.Pos.y) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
            //nếu cùng x: đi trái thì dùng ô bên dưới
            if (card.Pos.x < heroCard.Pos.x)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    if (positions[i].y < heroCard.Pos.y) return GridManager.Instance.dicGrids[positions[i]].card;
                }
            }
        }
        return GridManager.Instance.dicGrids[positions.RandomElement()].card;
    }

    public void UseCard(Card card)
    {
        var effect = card.cardEffect;
        if (effect != null)
        {
            effect.ApplyEffect(heroCard.GetComponent<Hero>());
        }
        //do something
        CardManager.Instance.MoveCardsAfterUse(card);
        var turnEnds = hero.GetComponentsInChildren<TurnEndEffect>();
        for (int i = 0; i < turnEnds.Length; i++)
        {
            turnEnds[i].OnTurnEnd();
        }
    }

    public void ShowDebug(List<Vector2Int> positions)
    {
        if (SHOW_DEBUG == false) return;
        for (int i = 0; i < positions.Count; i++)
        {
            var obj = Instantiate(test, GridManager.Instance.dicGrids[positions[i]].transform.position, Quaternion.identity);
            testPositions.Add(obj);
        }
    }
}

public class HeroManager : MonoBehaviour
{
    public static HeroManager Instance;
    public Hero hero;
    public Card heroCard;

    private void Awake()
    {
        
    }
}