using UnityEngine;

public class PowerupBase : MonoBehaviour
{
    public const float MOVE_TIME = 0.5f;
    public Vector2Int pos;
    public Card card;
    public ButtonPowerup buttonPowerup;

    public virtual void OnClick()
    {
        buttonPowerup.isUsingSkill = false;
        Hero hero = CardManager.Instance.hero;
        hero.canMove = true;
    }
}