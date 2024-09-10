using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPos : MonoBehaviour
{
    public const bool TEST_MODE = true;

    public Vector2Int pos;
    public TextMeshProUGUI txtDebug;
    public Card card;

    private void Awake()
    {
        txtDebug = GetComponentInChildren<TextMeshProUGUI>();
        txtDebug.text = "";
    }

    private void Update()
    {
        if (Constants.SHOW_CHEAT_OBJECT == false) return;
        txtDebug.text = pos.ToString();
        if (card == null) return;
        if (card.Pos != pos)
        {
            Debug.LogError($"wrong position at {card}, pos = {pos}");
        }
    }
}