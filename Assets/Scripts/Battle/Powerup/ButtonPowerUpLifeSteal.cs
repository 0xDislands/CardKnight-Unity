using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerUpLifeSteal : ButtonPowerup
{
    public PowerUpLifeSteal powerupPrefab;
    public override void OnClick()
    {
        if (isUsingSkill)
        {
            ResetSkill();
            return;
        }
        if (IsCooldownReady() == false) {
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            return;
        }
        isUsingSkill = true;
        this.hero.canMove = false;
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;

        var hero = CardManager.Instance.heroCard;
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            if (GridManager.Instance.grids[i].pos == hero.Pos) continue;
            if (GridManager.Instance.grids[i].card.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
            GridPos grid = GridManager.Instance.grids[i];
            var swap = Instantiate(powerupPrefab, grid.card.transform);
            swap.transform.position = grid.card.transform.position;
            swap.pos = grid.pos;
            swap.card = grid.card;
            swap.buttonPowerup = this;
        }
    }

    public override void ResetSkill()
    {
        base.ResetSkill();
        var powerUp = FindObjectsOfType<PowerUpLifeSteal>();
        for (int i = 0; i < powerUp.Length; i++)
        {
            Destroy(powerUp[i]);
        }
    }
}
