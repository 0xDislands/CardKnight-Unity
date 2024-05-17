using UnityEngine;

public class ButtonPowerupSlash : ButtonPowerup
{
    public PowerupSlash powerupPrefab;

    public override void OnClick()
    {
        if (isUsingSkill)
        {
            ResetSkill();
            return;
        }
        if (IsCooldownReady() == false)
        {
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            return;
        }
        isUsingSkill = true;
        this.hero.canMove = false;
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;

        var neightbours = CardManager.Instance.heroNeighbours;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            if (grid.card.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
            var slash = Instantiate(powerupPrefab, grid.card.transform);
            slash.transform.position = grid.card.transform.position;
            slash.pos = grid.pos;
            slash.card = grid.card;
            slash.buttonPowerup = this;
            CardManager.Instance.UpdateHeroNeighbours();
        }
        Hero hero = CardManager.Instance.hero;
        hero.canMove = false;
    }
    public override void ResetSkill()
    {
        base.ResetSkill();
        var neightbours = CardManager.Instance.heroNeighbours;
        hero.canMove = true;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            var powerupSlash = grid.card.GetComponentInChildren<PowerupSlash>();
            if (powerupSlash) Destroy(powerupSlash.gameObject);
        }
    }
}