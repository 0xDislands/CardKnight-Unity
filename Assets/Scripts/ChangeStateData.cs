using Unity.VisualScripting.Dependencies.NCalc;
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

    public string Title
    {
        get {
            var loseHp = Mathf.FloorToInt((float)(CardManager.Instance.hero.GetHP() * amount) / 100);
            if (loseHp == 0) loseHp = 1;

            var increaseExp = Mathf.FloorToInt((float)(CardManager.Instance.hero.GetHP() * amount) / 100);
            var hero = CardManager.Instance.hero;
            return id switch
            {
                LevelUpId.ADD_AMOUR => amount >= 999 ? $"Full Armor" : $"+ {amount} Max Armor",
                LevelUpId.ADD_HP => amount>= 999 ? $"Full Heal" : $"Heal {amount} HP",
                LevelUpId.INCREASE_EXP => $"Gain {Mathf.RoundToInt(hero.exp.expRequire * 0.5f)} Exp",
                LevelUpId.INCREASE_MAX_AMOUR_PERCENT => $"+{Mathf.RoundToInt(hero.heroData.maxShield * amount)} Max Armor",
                LevelUpId.INCREASE_MAX_HP_PERCENT => $"+{Mathf.RoundToInt(hero.heroData.maxHp * amount)} Max HP",
                LevelUpId.LOSE_CURRENT_HP_PERCENT => $"Lose {loseHp} HP",
                LevelUpId.ADD_HP_PERCENT => $"Heal {Mathf.RoundToInt(hero.heroData.hp * amount)} HP",
                LevelUpId.LOSE_POINT_PERCENT => $"Lose {Mathf.RoundToInt(Gameplay.Instance.Score * amount)} of your curren point",
                LevelUpId.RESET_COOL_DOWN_ALL_SKILL => $"Reset cooldown of all skills",
                _ => string.Empty
            };
        }
    }
}


