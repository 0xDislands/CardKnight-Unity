using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;
    public PopupLevelUp popupLevelUp;

    private void Awake()
    {
        Instance = this;
    }
}