using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dislands;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
using System;

public enum GameMode
{
    Normal,
    BossMode
}

[System.Serializable]
public class CardSpawnData
{
    public CardId cardId;
    public Dictionary<TagType, bool> tagDic;
}

public class CardManager : MonoBehaviour
{
    public const float DELAY_FLIP_TIME = 0.55f;
    public readonly Vector3 DEFAULT_SCALE = new Vector3(0.95f, 0.95f, 0.95f);
    public static CardManager Instance;
    public static HeroId selectedHero;

    [SerializeField] private Sprite cardBack;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Transform cardParent;
    [SerializeField] private TextAsset spawnCardData;
    public Hero hero;
    public Card heroCard;
    public bool canClick = true;
    public GameMode gameMode { get; private set; } = GameMode.Normal;

    public List<Card> cards;
    public List<Vector2Int> heroNeighbours { get; private set; } = new List<Vector2Int>();

    [SerializeField] private GameObject test;
    [SerializeField] private List<GameObject> testPositions = new List<GameObject>();
    private readonly List<Vector2Int> fourDirections = new List<Vector2Int>()
    {
        new Vector2Int(0,1),
        new Vector2Int(0,-1),
        new Vector2Int(1,0),
        new Vector2Int(-1,0)
    };
    List<CardId> startCards;
    [SerializeField] List<CardId> spawnCards;
    [SerializeField] List<CardSpawnData> spawnCardsData;
    int spawnCardIndex = -1;
    private void Awake()
    {
        Instance = this;
    }

    public void StartGame()
    {
        startCards = GetStartCards();
        spawnCards = GetSpawnCards();
        spawnCardsData = GetSpawnCardsData();
        SpawnAllCard();
        UpdateHeroNeighbours();
        StartCoroutine(IECardAnimation());
        var heroData = DataManager.Instance.dicHero[selectedHero];
        heroCard.icon.sprite = heroData.skins[heroData.selectedSkin];
        heroCard.icon.preserveAspect = true;
    }

    private void Update()
    {
        if (hero.heroData.hp == 0)
        {
            Gameplay.Instance.Lose();
        }
        if (cards.Count > 9)
        {
            Debug.LogError("card is more than 9");
        }
        for (int i = 0; i < heroNeighbours.Count; i++)
        {
            Debug.DrawLine(heroCard.transform.position, GridManager.Instance.dicGrids[heroNeighbours[i]].transform.position, Color.red);
        }
    }

    private void SpawnAllCard()
    {
        for (int i = 0; i < cardParent.childCount; i++)
        {
            cardParent.GetChild(i).gameObject.SetActive(false);
        }
        //cần spawn hero đầu tiên vì các card khác cần lấy data từ hero (ví dụ như card monster)
        int midIndex = GridManager.Instance.grids.Length / 2;
        cards = new List<Card>();
        heroCard = SpawnCard(GridManager.Instance.grids[midIndex].pos, CardId.Hero);
        heroCard.icon.sprite = DataManager.Instance.dicHero[selectedHero].sprite;
        hero = heroCard.GetComponent<Hero>();
        int startCardIndex = 0;

        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            Card card;
            CardId id;
            if (i == midIndex)
            {
                card = heroCard;
            } else
            {
                id = startCards[startCardIndex];
                card = SpawnCard(GridManager.Instance.grids[i].pos, id);
                startCardIndex++;
                if(card.TryGetComponent<Monster>(out var monster))
                {
                    foreach (var item in monster.tags)
                    {
                        item.gameObject.SetActive(false);
                    }
                }
            }
            card.name = "Card" + i;
            card.transform.position = new Vector2(999f, 999f);
        }
        //cần clear list để add lại theo đúng thứ tự
        cards = new List<Card>();
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            Card card = GridManager.Instance.grids[i].card;
            cards.Add(card);
        }
    }

    private List<CardId> GetStartCards()
    {
        List<CardId> results = new List<CardId>();
        List<CardId> monsters = new List<CardId>()
        {
            CardId.Monster1,CardId.Monster1, CardId.Monster2, CardId.Monster2
        };
        results.AddRange(monsters);
        List<CardId> nonMonsters = new List<CardId>()
        {
            CardId.ItemHeal, CardId.ItemChest, CardId.ItemShield
        };
        for (int i = 0; i < 4; i++)
        {
            results.Add(nonMonsters.RandomElement());
        }
        results.Shuffle();
        return results;
    }
    private List<CardId> GetSpawnCards()
    {
        if (Constants.TEST_SKILL_FIRE)
        {
            return new List<CardId>() {
            CardId.Monster1, CardId.SkillFire,
            CardId.Monster2, CardId.SkillFire};
        }
        var contents = spawnCardData.text.Split("\n");
        Dictionary<string, CardId> dicCard = new Dictionary<string, CardId>();
        dicCard.Add("1", CardId.Monster1);
        dicCard.Add("2", CardId.Monster2);
        dicCard.Add("3", CardId.Monster3);
        dicCard.Add("4", CardId.Boss1);
        dicCard.Add("101", CardId.ItemHeal);
        dicCard.Add("102", CardId.ItemPoison);
        dicCard.Add("103", CardId.ItemChest);
        dicCard.Add("104", CardId.ItemChestMiniGame);
        dicCard.Add("105", CardId.ItemChestEvil);
        dicCard.Add("106", CardId.ItemShield);
        dicCard.Add("107", CardId.SkillFire);

        List<CardId> spawnCards = new List<CardId>();
        var spawnData = contents[0].Replace("\r","");
        string[] lines = spawnData.Split("\t");
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == null) continue;
            lines[i] = lines[i].Replace(" ", "").Replace("\r", "").Replace("\t", "");
            if (lines[i] == "") continue;
            if (dicCard.ContainsKey(lines[i]) == false)
            {
                Debug.LogWarning("not found key with id : " + lines[i]);
                continue;
            } else
            {
                spawnCards.Add(dicCard[lines[i]]);
            }
        }
        return spawnCards;
    }

    private List<CardSpawnData> GetSpawnCardsData()
    {
        var allTag = (TagType[])Enum.GetValues(typeof(TagType));
        var data = string.Empty;
        if (EditData.useCustomData && !string.IsNullOrEmpty(EditData.dataStr)) data = EditData.dataStr;
        else data = spawnCardData.text;
        var contents = data.Split("\n");
        Dictionary<string, CardId> dicCard = new Dictionary<string, CardId>();
        dicCard.Add("1", CardId.Monster1);
        dicCard.Add("2", CardId.Monster2);
        dicCard.Add("3", CardId.Monster3);
        dicCard.Add("4", CardId.Boss1);
        dicCard.Add("5", CardId.MonsterGhost);
        dicCard.Add("6", CardId.Boss2);
        dicCard.Add("101", CardId.ItemHeal);
        dicCard.Add("102", CardId.ItemPoison);
        dicCard.Add("103", CardId.ItemChest);
        dicCard.Add("104", CardId.ItemChestMiniGame);
        dicCard.Add("105", CardId.ItemChestEvil);
        dicCard.Add("106", CardId.ItemShield);
        dicCard.Add("107", CardId.SkillFire);
        dicCard.Add("108", CardId.ItemSpike);

        List<CardSpawnData> spawnCards = new List<CardSpawnData>();
        var spawnData = contents[0].Replace("\r", "");
        var growthData = contents[1].Replace("\r", "").Split("\t");
        var noMagic = contents[2].Replace("\r", "").Split("\t");
        var revenge = contents[3].Replace("\r", "").Split("\t");
        var noHeal = contents[4].Replace("\r", "").Split("\t");
        var noSkill = contents[5].Replace("\r", "").Split("\t");
        string[] lines = spawnData.Split("\t");
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i] == null) continue;
            lines[i] = lines[i].Replace(" ", "").Replace("\r", "").Replace("\t", "");
            if (lines[i] == "") continue;
            if (dicCard.ContainsKey(lines[i]) == false)
            {
                Debug.LogError("not found key with id : " + lines[i]);
                continue;
            } else
            {
                var newData = new CardSpawnData();
                newData.cardId = dicCard[lines[i]];
                newData.tagDic = new Dictionary<TagType, bool>();
                foreach (var item in allTag)
                {
                    if(item == TagType.Growth)
                    {
                        var active = growthData[i] == "0" ? false : true;
                        newData.tagDic.Add(item, active);
                    }
                    else if(item == TagType.NoMagic)
                    {
                        var active = noMagic[i] == "0" ? false : true;
                        newData.tagDic.Add(item, active);
                    } else if (item == TagType.Revenge)
                    {
                        var active = revenge[i] == "0" ? false : true;
                        newData.tagDic.Add(item, active);
                    } else if (item == TagType.NoHope)
                    {
                        var active = noHeal[i] == "0" ? false : true;
                        newData.tagDic.Add(item, active);
                    } else if (item == TagType.Silient)
                    {
                        var active = noSkill[i] == "0" ? false : true;
                        newData.tagDic.Add(item, active);
                    }
                }
                spawnCards.Add(newData);
            }
        }

        return spawnCards;
    }


    public CardSpawnData GetNextCard()
    {
        spawnCardIndex++;
        if (spawnCardIndex >= spawnCards.Count)
        {
            spawnCardIndex = 0;
        }
        return spawnCardsData[spawnCardIndex];
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


    public Card SpawnCard(Vector2Int pos, CardId id)
    {
        var data = DataManager.Instance.dicCardDatas[id];
        var grid = GridManager.Instance.dicGrids[pos];
        if (data.cardPrefab == null)
        {
            Debug.LogError("null data.cardPrefab");
        }
        var card = Instantiate(data.cardPrefab, cardParent);
        card.transform.localScale = DEFAULT_SCALE;
        card.Pos = grid.pos;
        card.transform.position = grid.transform.position;
        card.gameObject.name = "Card #" + UnityEngine.Random.Range(100, 200);
        card.SetData(data);
        if (cards.Contains(card) == false)
        {
            cards.Add(card);
        }
        EventManager.Instance.onNewCardSpawned?.Invoke(card);
        return card;
    }

    public bool IsNextToHeroCard(Card card)
    {
        return heroNeighbours.Contains(card.Pos);
    }

    public bool IsNextToHeroCard(Vector2Int pos)
    {
        return heroNeighbours.Contains(pos);
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

    public void UpdateHeroNeighbours()
    {
        heroNeighbours = GetNeightbourPositions(heroCard.Pos);
    }

    public void MoveCardsAfterUse(Card card)
    {
        hero.hasMove = heroCard.Pos != card.Pos;
        var moveCard = GetMoveCard(card);
        var spawnNewCardPosition = moveCard.Pos;
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
        var newCardId = GetNextCard();
        var newCard = SpawnCard(spawnNewCardPosition, newCardId.cardId);
        newCard.ShowSpawnAnimation(0f);
        if (newCard.TryGetComponent<Monster>(out var monster))
        {
            monster.SetTag(newCardId.tagDic);
        }
        UpdateHeroNeighbours();
        EventManager.Instance.OnHeroMove?.Invoke();
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
            //nếu cùng y: đi trái thì dùng ô bên dưới
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
        if (canClick == false) return;
        StartCoroutine(IEUseCard(card));
    }

    IEnumerator IEUseCard(Card card)
    {
        var effect = card.cardEffect;
        if (effect != null)
        {
            if (gameMode == GameMode.BossMode && card.side == CardSide.Back)
            {
                card.FlipToFront();
                yield return new WaitForSeconds(DELAY_FLIP_TIME);
            }
            effect.ApplyEffect(hero);
        }
        foreach (var item in Gameplay.Instance.buttonPowerups)
        {
            if (item.IsUnlocked()) item.TurnLeftToUSeSkill--;
        }
        yield return IETurnEnd();
    }

    public IEnumerator IETurnEnd()
    {
        var turnEndsHero = hero.GetComponentsInChildren<TurnEndEffect>();
        for (int i = 0; i < turnEndsHero.Length; i++)
        {
            yield return turnEndsHero[i].IETurnEnd();
        }
    }
    
    public void GrowAllMonster()
    {
        var tag = FindTag(TagType.Growth);
        if (tag != null) StartCoroutine(tag.IETurnEnd());
    }

    public MonsterTag FindTag(TagType type)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].TryGetComponent<Monster>(out var monster))
            {
                var tags = monster.GetComponentsInChildren<MonsterTag>(true);
                foreach (var tag in tags)
                {
                    if (tag.type == type && tag.gameObject.activeInHierarchy)
                    {
                        return tag;
                    }
                }
            }
        }
        return null;
    }


    public void ShowDebug(List<Vector2Int> positions)
    {

    }

    internal void RemoveCard(Card card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
        } else
        {
            Debug.LogWarning("card not exist in cards");
        }
    }

    public void SetMode(GameMode mode)
    {
        gameMode = mode;
    }

    public void FlipDownAllCards()
    {
        StartCoroutine(IEFlipDownAllCards());
    }

    private IEnumerator IEFlipDownAllCards()
    {
        List<Card> flipBackCards = new List<Card>();
        flipBackCards.AddRange(this.cards);
        flipBackCards.Remove(CardManager.Instance.heroCard);

        for (int i = flipBackCards.Count - 1; i >= 0; i--)
        {
            var boss = flipBackCards[i].GetComponent<Boss>();
            if (boss != null) flipBackCards.Remove(flipBackCards[i]);
        }
        for (int i = 0; i < flipBackCards.Count; i++)
        {
            if (flipBackCards[i].side == CardSide.Front) flipBackCards[i].FlipToBack();
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void EndBossMode()
    {
        StartCoroutine(IEEndBossMode());
    }

    private IEnumerator IEEndBossMode()
    {
        gameMode = GameMode.Normal;
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].side == CardSide.Back) cards[i].FlipToFront();
            yield return new WaitForSeconds(0.1f);
        }
    }
}