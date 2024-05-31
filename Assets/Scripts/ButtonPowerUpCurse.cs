using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerUpCurse : ButtonPowerup
{
    public PowerupCurse powerupPrefab;
    public override void OnClick()
    {
        if (SkillDisable())
        {
            var text = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            text.text.text = "Silent"; SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("Silent"), transform.position + new Vector3(0, 1f));
            return;
        }
        if (isUsingSkill)
        {
            CancelSkill();
            return;
        }
        if (IsLevelReady() == false)
        {
            Notify("LEVEL UP TO UNLOCK");
            return;
        }
        if (IsCooldownReady() == false)
        {
            Notify("ON COOLDOWN");
            return;
        }
        isUsingSkill = true;
        this.hero.canMove = false;
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;

        var hero = CardManager.Instance.heroCard;
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            if (GridManager.Instance.grids[i].pos == hero.Pos) continue;
            //if (GridManager.Instance.grids[i].card.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
            //if (GridManager.Instance.grids[i].pos == hero.Pos) continue;
            GridPos grid = GridManager.Instance.grids[i];
            var curse = Instantiate(powerupPrefab, grid.card.transform);
            curse.transform.position = grid.card.transform.position;
            curse.pos = grid.pos;
            curse.card = grid.card;
            curse.buttonPowerup = this;
        }
    }

    public override void CancelSkill()
    {
        base.CancelSkill();
        var powerUp = FindObjectsOfType<PowerupCurse>();
        for (int i = 0; i < powerUp.Length; i++)
        {
            Destroy(powerUp[i].gameObject);
        }
    }
}
