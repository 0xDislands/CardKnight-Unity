using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerUpLifeSteal : ButtonPowerup
{
    public PowerUpLifeSteal powerupPrefab;
    public override void OnClick()
    {
        if (CanUse() == false) return;
        CurrentAtkTime = 0;

        var hero = CardManager.Instance.heroCard;
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            if (GridManager.Instance.grids[i].pos == hero.Pos) continue;
            GridPos grid = GridManager.Instance.grids[i];
            var swap = Instantiate(powerupPrefab, grid.card.transform);
            swap.transform.position = grid.card.transform.position;
            swap.pos = grid.pos;
            swap.card = grid.card;
        }
    }
}
