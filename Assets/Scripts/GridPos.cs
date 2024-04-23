using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPos : MonoBehaviour
{
    public const bool TEST_MODE = true;

    public Vector2Int pos;
    public TextMeshPro txtDebug;
    public Card card;

    private void Awake()
    {
        txtDebug = GetComponentInChildren<TextMeshPro>();
    }

    private void Update()
    {
        txtDebug.text = card == null || TEST_MODE == false ? "" : pos.ToString();
    }
}