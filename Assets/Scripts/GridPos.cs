using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPos : MonoBehaviour
{
    public const bool TEST_MODE = true;

    public Vector2Int gridPosition;
    public TextMeshProUGUI txtDebug;
    public Card card;

    private void Awake()
    {
        txtDebug = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        txtDebug.text = gridPosition.ToString();
    }
}