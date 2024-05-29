﻿using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextHp : MonoBehaviour
{
    private TextMeshProUGUI txtHp;

    private void Awake()
    {
        txtHp = GetComponent<TextMeshProUGUI>();
    }
    public void SetHP(HeroBattleData heroData)
    {
        txtHp.text = heroData.hp + "/" + heroData.maxHp;
    }
    public void SetHP(MonsterData monsterData)
    {
        txtHp.text = (Mathf.RoundToInt(monsterData.currentHp)).ToString();
    }
}