#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridManager))]
public class GridManagerCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GridManager grid = (GridManager)target;
        if (GUILayout.Button("SET UP GRID", GUILayout.Width(150), GUILayout.Height(50))) 
        {
            grid.Init();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    //public void SnapToGridAll(GridManager grid)
    //{
    //    grid.Awake();
    //    BlockCell[] cells = grid.transform.parent.GetComponentsInChildren<BlockCell>();
    //    for (int i = 0; i < cells.Length; i++)
    //    {
    //        cells[i].Awake();
    //    }
    //    Block[] blocks = grid.transform.parent.GetComponentsInChildren<Block>();
    //    for (int i = 0; i < blocks.Length; i++)
    //    {
    //        blocks[i].Awake();
    //        blocks[i].Init();
    //    }
    //}
}

#endif