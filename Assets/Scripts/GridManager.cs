using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DarkcupGames;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public int WIDTH = 3;

    public GridPos[] grids;
    public Dictionary<Vector2Int, GridPos> dicGrids = new Dictionary<Vector2Int, GridPos>();
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




//public class GridManager : MonoBehaviour
//{
//    public static GridManager Instance;

//    public GridPos gridPrefab;

//    public int width = 5;
//    public int height = 5;
//    public float paddingX = 1f;
//    public float paddingY = 1f;
//    public float cameraSize = 3.15f;
//    public float topY;
//    public float botY;
//    public float topX;
//    public float botX;
//    public Vector2 bottomLeft;
//    public Vector2 topRight;

//    [HideInInspector] public Dictionary<Vector2Int, GridPos> dicGrids = new Dictionary<Vector2Int, GridPos>();
//    [HideInInspector] public GridPos[] grids;
//    [HideInInspector] public GridPosEditor[] gridEditors;
//    [HideInInspector] public Dictionary<Vector2Int, GridPosEditor> dicGridEditors = new Dictionary<Vector2Int, GridPosEditor>();

//    public void Awake()
//    {
//        Instance = this;

//        dicGrids = new Dictionary<Vector2Int, GridPos>();
//        dicGridEditors = new Dictionary<Vector2Int, GridPosEditor>();
//        grids = GetComponentsInChildren<GridPos>();
//        gridEditors = GetComponentsInChildren<GridPosEditor>();

//        for (int i = 0; i < grids.Length; i++)
//        {
//            dicGrids.Add(grids[i].pos, grids[i]);
//            if (grids[i].transform.localPosition.y > topY) topY = grids[i].transform.localPosition.y;
//            if (grids[i].transform.localPosition.y < botY) botY = grids[i].transform.localPosition.y;
//            if (grids[i].transform.localPosition.x > topX) topX = grids[i].transform.localPosition.x;
//            if (grids[i].transform.localPosition.x < botX) botX = grids[i].transform.localPosition.x;
//        }
//        //GenerateDicNeighbours();

//        bottomLeft = dicGrids[new Vector2Int(0,0)].transform.position;
//        topRight = dicGrids[new Vector2Int(width - 1, height - 1)].transform.position;
//    }

//    //private void Start()
//    //{
//    //    var cameraWidth = Camera.main.GetComponent<CameraFitWidth>();
//    //    //cameraWidth.ratio = cameraSize;
//    //    cameraWidth.ratio = Constants.CAMERA_SIZE;
//    //    cameraWidth.UpdateSize();
//    //}

//    [ContextMenu("Generate Grid")]
//    public void GenerateGrid()
//    {
//        Debug.Log("Destroy old grid!");
//        GridPos[] grids = GetComponentsInChildren<GridPos>();
//        for (int i = 0; i < grids.Length; i++)
//        {
//            DestroyImmediate(grids[i].gameObject);
//        }
//        Debug.Log("Generating new grid!");

//        Vector2 startPos = transform.position -new Vector3(width / 2 * paddingX, height / 2 * paddingY);

//        for (int i = 0; i < width; i++)
//        {
//            for (int j = 0; j < height; j++)
//            {
//                Vector2 pos = startPos + new Vector2(i * paddingX, j * paddingY) ;
//                var grid = Instantiate(gridPrefab, transform);
//                grid.transform.position = pos;
//                grid.pos = new Vector2Int(i, j);
//            }
//        }
//    }

//    /// <summary>
//    /// Điều chỉnh velocity của block lại để block chỉ có thể di chuyển trong grid chứ không đi ra ngoài grid
//    /// </summary>
//    /// <param name="block"></param>
//    /// <returns></returns>
//    //public Vector2 MoveInsideArea(Block block)
//    //{
//    //    Vector2 direction = block.rb2D.velocity;

//    //    if (block.left.transform.position.x < bottomLeft.x && direction.x < 0) {
//    //        direction.x = 0;
//    //    } 
//    //    if (block.right.transform.position.x > topRight.x && direction.x > 0)
//    //    {
//    //        direction.x = 0;
//    //    }
//    //    if (block.down.transform.position.y < bottomLeft.y && direction.y < 0)
//    //    {
//    //        direction.y = 0;
//    //    }
//    //    if (block.top.transform.position.y > topRight.y && direction.y > 0)
//    //    {
//    //        direction.y = 0;
//    //    }
//    //    block.rb2D.velocity = direction;
//    //    return direction;
//    //}

//    //public void SnapToGridAll()
//    //{
//    //    this.Awake();
//    //    BlockCell[] cells = this.transform.parent.GetComponentsInChildren<BlockCell>();
//    //    for (int i = 0; i < cells.Length; i++)
//    //    {
//    //        cells[i].Awake();
//    //    }
//    //    Block[] blocks = this.transform.parent.GetComponentsInChildren<Block>();
//    //    for (int i = 0; i < blocks.Length; i++)
//    //    {
//    //        blocks[i].Awake();
//    //        blocks[i].Init();
//    //    }
//    //}

//    //[ContextMenu("Set up Grid")]
//    //public void SetUpGridEditor()
//    //{
//    //    GenerateGrid();
//    //    SnapToGridAll();
//    //    var blocks = transform.parent.GetComponentsInChildren<Block>();
//    //    for (int i = 0; i < blocks.Length; i++)
//    //    {
//    //        blocks[i].SetPivotCenterObject();
//    //    }
//    //    var pairs = transform.parent.GetComponentsInChildren<PairController>();
//    //    for (int i = 0; i < pairs.Length; i++)
//    //    {
//    //        pairs[i].CalculateDistance();
//    //    }

//    //    const int SCREEN_WIDTH = 1080;
//    //    const int SCREEN_HEIGHT = 1920;

//    //    Screen.SetResolution(SCREEN_WIDTH, SCREEN_HEIGHT, false);
//    //    //Camera.main.orthographicSize = cameraSize * SCREEN_HEIGHT / SCREEN_WIDTH;
//    //    Camera.main.orthographicSize = Constants.CAMERA_SIZE * SCREEN_HEIGHT / SCREEN_WIDTH;
//    //}

//    //public void GenerateDicNeighbours()
//    //{
//    //    dicNeightbours = new Dictionary<NeightbourData, TileDescription>();
//    //    dicNeightbours.Add(new NeightbourData(false, false, false, false), new TileDescription(TilePositionType.Single, 0));

//    //    List<NeightbourData> list = new List<NeightbourData>();
//    //    list.Add(new NeightbourData(false, true, false, true)); //false true false true
//    //    list.Add(new NeightbourData(false, true, true, true)); //false true true true
//    //    list.Add(new NeightbourData(false, true, true, false)); //false true true false
//    //    list.Add(new NeightbourData(true, true, false, true)); //true true false true
//    //    list.Add(new NeightbourData(true, true, true, true)); //true true true true
//    //    list.Add(new NeightbourData(true, true, true, false)); //true true true false
//    //    list.Add(new NeightbourData(true, false, false, true)); //true false false true
//    //    list.Add(new NeightbourData(true, false, true, true)); //true false true true
//    //    list.Add(new NeightbourData(true, false, true, false)); //true false true false

//    //    for (int i = 0; i < list.Count; i++)
//    //    {
//    //        TileDescription desc = new TileDescription();
//    //        desc.type = TilePositionType.Big;
//    //        desc.index = i;
//    //        dicNeightbours.Add(list[i], desc);
//    //    }

//    //    list = new List<NeightbourData>();
//    //    list.Add(new NeightbourData(false, true, false, false)); //False True False False
//    //    list.Add(new NeightbourData(true, true, false, false)); //True True False False
//    //    list.Add(new NeightbourData(true, false, false, false)); //True False False False

//    //    for (int i = 0; i < list.Count; i++)
//    //    {
//    //        TileDescription vertical = new TileDescription();
//    //        vertical.type = TilePositionType.Vertical;
//    //        vertical.index = i;
//    //        dicNeightbours.Add(list[i], vertical);
//    //    }

//    //    list = new List<NeightbourData>();
//    //    list.Add(new NeightbourData(false, false, false, true)); //False False False True
//    //    list.Add(new NeightbourData(false, false, true, true)); //False False True True
//    //    list.Add(new NeightbourData(false, false, true, false)); //False False True False

//    //    for (int i = 0; i < list.Count; i++)
//    //    {
//    //        TileDescription horizontal = new TileDescription();
//    //        horizontal.type = TilePositionType.Horizontal;
//    //        horizontal.index = i;
//    //        dicNeightbours.Add(list[i], horizontal);
//    //    }
//    //}

//    public static GridPos GetNearestGrid(Vector2 pos)
//    {
//        Vector2 startPos = Instance.dicGrids[new Vector2Int(0, 0)].transform.position;
//        float x = (pos.x - startPos.x) / Instance.paddingX;
//        float y = (pos.y - startPos.y) / Instance.paddingX;
//        Vector2Int cord = new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
//        if (Instance.dicGrids.ContainsKey(cord))
//        {
//            return Instance.dicGrids[cord];
//        } else
//        {
//            return null;
//        }
//    }

//    public static GridPos GetNearestGrid(Vector2 pos, Transform pivot, float maxDistance)
//    {
//        float nearestDistance = Mathf.Infinity;
//        int nearest = 0;

//        for (int i = 0; i < Instance.grids.Length; i++)
//        {
//            float distance = Vector2.Distance(pos, Instance.grids[i].transform.position);

//            if (distance < nearestDistance && Vector2.Distance(pos, pivot.transform.position) < maxDistance)
//            {
//                nearest = i;
//                nearestDistance = distance;
//            }
//        }
//        return Instance.grids[nearest];
//    }

//    public static Vector3 ConvertGridPosToWorldPos(Vector2Int gridPos)
//    {
//        return Instance.dicGrids[gridPos].transform.position;
//    }

//    public void UpdateDisplay()
//    {
//        //for (int i = 0; i < gridEditors.Length; i++)
//        //{
//        //    gridEditors[i].UpdateDisplay();
//        //}
//    }

//    public void Replay()
//    {
//        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//    }
//}