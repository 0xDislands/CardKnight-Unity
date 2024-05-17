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
    [SerializeField] private string title;
    public string description;

    public string Title
    {
        get { 
            return id switch
            {
                LevelUpId.ADD_AMOUR => $"Add {amount} armour",
                LevelUpId.ADD_HP => amount>=9999 ? $"Heal Full HP" : $"Heal {amount} HP",
                LevelUpId.INCREASE_EXP => $"Add {Mathf.RoundToInt(CardManager.Instance.hero.exp.expRequire * 0.5f)} Exp",
                LevelUpId.INCREASE_MAX_AMOUR => $"Add {amount} Max Armour",
                LevelUpId.INCREASE_MAX_HP => $"Add {amount} Max HP",
                LevelUpId.LOSE_CURRENT_HP_PERCENT => $"Lose {amount}% Max HP",
                _ => string.Empty
            };
        }
    }
}
