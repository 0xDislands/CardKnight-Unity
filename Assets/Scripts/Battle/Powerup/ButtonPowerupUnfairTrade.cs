﻿public class ButtonPowerupUnfairTrade : ButtonPowerup
{
    public PowerupUnfairTrade powerupPrefab;

    public override void OnClick()
    {
        if (active)
        {
            ResetSkill();
            return;
        }
        if (IsCooldownReady() == false) return;
        active = true;
        this.hero.canMove = false;
        CurrentAtkTime = atkToAvailable;

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
    public override void ResetSkill()
    {
        base.ResetSkill();
        var powerUp = FindObjectsOfType<PowerupUnfairTrade>();
        for (int i = 0; i < powerUp.Length; i++)
        {
            Destroy(powerUp[i]);
        }
    }
}