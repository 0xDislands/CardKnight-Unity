using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<CardData> cardDatas = new List<CardData>();
    public List<PowerupData> powerupDatas = new List<PowerupData>();
    public List<HeroData> heroDatas = new List<HeroData>();
    public List<CardData> noneHeroCardDatas { get; private set; } = new List<CardData>();
    public Dictionary<CardId, CardData> dicCardDatas { get; private set; } = new Dictionary<CardId, CardData>();
    public Dictionary<PowerupId, PowerupData> dicPowerUp { get; private set; } = new Dictionary<PowerupId, PowerupData>();
    public Dictionary<HeroId, HeroData> dicHero { get; private set; } = new Dictionary<HeroId, HeroData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Init();
    }
    public void Init()
    {
        dicCardDatas = new Dictionary<CardId, CardData>();
        for (int i = 0; i < cardDatas.Count; i++)
        {
            if (dicCardDatas.ContainsKey(cardDatas[i].id)== false)
            {
                dicCardDatas.Add(cardDatas[i].id, cardDatas[i]);
            } else
            {
                Debug.LogError($"duplicate id {cardDatas[i].id}");
            }
            if (noneHeroCardDatas.Contains(cardDatas[i]) == false && cardDatas[i].id != CardId.Hero){
                noneHeroCardDatas.Add(cardDatas[i]);
            }
        }
        dicPowerUp = new Dictionary<PowerupId, PowerupData>();
        for (int i = 0; i < powerupDatas.Count; i++)
        {
            dicPowerUp.Add(powerupDatas[i].id, powerupDatas[i]);
        }
        dicHero = new Dictionary<HeroId, HeroData>();
        for (int i = 0; i < heroDatas.Count; i++)
        {
            dicHero.Add(heroDatas[i].id, heroDatas[i]);
        }
    }
}