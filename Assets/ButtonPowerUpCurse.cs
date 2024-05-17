using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerUpCurse : ButtonPowerup
{
    public PowerupCurse powerupPrefab;
    public override void OnClick()
    {
        if (IsCooldownReady() == false) 
        {
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            return;
        }
        if (isUsingSkill)
        {
            ResetSkill();
            return;
        }
        isUsingSkill = true;
        this.hero.canMove = false;
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;

        var hero = CardManager.Instance.heroCard;
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            if (GridManager.Instance.grids[i].card.GetComponentInChildren<ImueMagicTag>() != null) continue;
            if (GridManager.Instance.grids[i].pos == hero.Pos) continue;
            GridPos grid = GridManager.Instance.grids[i];
            var swap = Instantiate(powerupPrefab, grid.card.transform);
            swap.transform.position = grid.card.transform.position;
            swap.pos = grid.pos;
            swap.card = grid.card;
        }
    }

    public override void ResetSkill()
    {
        base.ResetSkill();
        var powerUp = FindObjectsOfType<PowerupCurse>();
        for (int i = 0; i < powerUp.Length; i++)
        {
            Destroy(powerUp[i]);
        }
    }
}
