using UnityEngine;

public enum LevelUpDataType
{
    Good, Bad
}

public class ChangeStateData : MonoBehaviour
{
    public LevelUpId id;
    public LevelUpDataType type;
    public int amount;
    public Sprite sprite;
    public string title;
    public string description;
}
