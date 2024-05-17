using UnityEngine;

public class PowerupBase : MonoBehaviour
{
    public const float MOVE_TIME = 0.5f;
    public Vector2Int pos;
    public Card card;
    public ButtonPowerup buttonPowerup;
    protected PowerupId id;

    public virtual void OnClick()
    {
        buttonPowerup.isUsingSkill = false;
        Hero hero = CardManager.Instance.hero;
        hero.canMove = true;
    }

    protected bool IsImuned()
    {
        var imune = card.GetComponentInChildren<ImmuneMagicTag>(true);
        return imune != null && imune.gameObject.activeInHierarchy;
    }
}