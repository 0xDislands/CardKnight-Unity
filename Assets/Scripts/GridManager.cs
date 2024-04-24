using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DarkcupGames;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public int WIDTH = 3;

    public GridPos[] grids { get; private set; }
    public Dictionary<Vector2Int, GridPos> dicGrids { get; private set; } = new Dictionary<Vector2Int, GridPos>();
    private void Awake()
    {
        Instance = this;
        Init();
    }
    public void Init()
    {
        grids = GetComponentsInChildren<GridPos>();
        dicGrids = new Dictionary<Vector2Int, GridPos>();
        for (int i = 0; i < grids.Length; i++)
        {
            int x = i % WIDTH;
            int y = i / WIDTH;
            grids[i].pos = new Vector2Int(x, y);
            dicGrids.Add(grids[i].pos, grids[i]);
        }
    }

    public bool IsInsideGrid(Vector2Int pos)
    {
        return dicGrids.ContainsKey(pos);
    }
}