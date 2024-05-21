using TMPro;
using UnityEngine;
public class ButtonPowerupHex : ButtonPowerup
{
    public PowerupHex powerupPrefab;
    public override void OnClick()
    {
        var neightbours = CardManager.Instance.heroNeighbours;
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

        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            if (grid.card.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
            if (!grid.card.TryGetComponent<Monster>(out var monster)) continue;
            monsterCount++;
            var hex = Instantiate(powerupPrefab, grid.card.transform);
            hex.transform.position = grid.card.transform.position;
            hex.pos = grid.pos;
            hex.card = grid.card;
            hex.buttonPowerup = this;
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
            PowerupHex powerupHex = grid.card.GetComponentInChildren<PowerupHex>();
            if (powerupHex) Destroy(powerupHex.gameObject);
        }
    }
}