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
            var displayHP = Mathf.FloorToInt((float)(CardManager.Instance.hero.GetHP() * amount) / 100);
            if (displayHP == 0) displayHP = 1;
            return id switch
            {
                LevelUpId.ADD_AMOUR => amount >= 9999 ? $"Add Max Armour" : $"+ {amount} Max Armour",
                LevelUpId.ADD_HP => amount>=9999 ? $"Full Heal" : $"Heal {amount} HP",
                LevelUpId.INCREASE_EXP => $"Gain {Mathf.RoundToInt(CardManager.Instance.hero.exp.expRequire * 0.5f)} Exp",
                LevelUpId.INCREASE_MAX_AMOUR => $"+{amount} Max Armour",
                LevelUpId.INCREASE_MAX_HP => $"+{amount} Max HP",
                LevelUpId.LOSE_CURRENT_HP_PERCENT => $"Lose {displayHP} HP",
                _ => string.Empty
            };
        }
    }
}
