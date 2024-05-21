using UnityEngine;

public class ButtonPowerupSlash : ButtonPowerup
{
    public PowerupSlash powerupPrefab;

    public override void OnClick()
    {
        if (isUsingSkill)
        {
            CancelSkill();
            return;
        }
        if (IsCooldownReady() == false)
        {
            Notify("ON COOLDOWN");
            return;
        }
        isUsingSkill = true;
        hero.canMove = false;
        int monsterCount = 0;
        var neightbours = CardManager.Instance.heroNeighbours;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            if (grid.card.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
            if (!grid.card.TryGetComponent<Monster>(out var monster)) continue;
            monsterCount++;
            var slash = Instantiate(powerupPrefab, grid.card.transform);
            slash.transform.position = grid.card.transform.position;
            slash.pos = grid.pos;
            slash.card = grid.card;
            slash.buttonPowerup = this;
        }
        if (monsterCount == 0)
        {
            Notify("OUT OF RANGE");
            hero.canMove = true;
        } else
        {
            TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        }
    }
    public override void CancelSkill()
    {
        base.CancelSkill();
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