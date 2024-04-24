using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public List<CardData> cardDatas = new List<CardData>();
    public List<CardData> noneHeroCardDatas = new List<CardData>();
    public Dictionary<CardId, CardData> dicCardDatas = new Dictionary<CardId, CardData>();
    private void Awake()
    {
        Instance = this;
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
    }
}