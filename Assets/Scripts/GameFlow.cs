using UnityEngine;

public class GameFlow : MonoBehaviour
{
    public static GameFlow Instance;
    public PopupLevelUp popupLevelUp;
    public PopupPoweupUnlocked popupPoweupUnlocked;

    private void Awake()
    {
        Instance = this;
    }
}