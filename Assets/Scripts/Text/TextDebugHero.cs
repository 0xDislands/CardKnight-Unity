﻿using UnityEngine;
using TMPro;

public class TextDebugHero : MonoBehaviour
{
    private TextMeshProUGUI txtDebug;

    private void Awake ()
    {
        txtDebug = GetComponent<TextMeshProUGUI>();
    }

    private void Update ()
    {
        txtDebug.text = GetDebugText ();
    }

    public string GetDebugText ()
    {
        var data = CardManager.Instance.hero.heroData;
        string str = "hero \n";
        str += $"hp: {data.hp} / {data.maxHp} \n";
        str += $"shield: {data.shield} / {data.maxShield} \n";
        str += $"exp: {data.currentExp} \n";
        str += $"level: {data.level + 1} \n";
        return str;
    }
}