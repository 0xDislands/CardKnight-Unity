using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPowerUpLifeSteal : ButtonPowerup
{
    public PowerUpLifeSteal powerupPrefab;
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
        if (IsCooldownReady() == false) {
            Notify("ON COOLDOWN");
            return;
        }
        isUsingSkill = true;
        hero.canMove = false;
        int monsterCount = 0;
        var heroCard = CardManager.Instance.heroCard;
        for (int i = 0; i < CardManager.Instance.cards.Count; i++)
        {
            if (GridManager.Instance.grids[i].pos == heroCard.Pos) continue;
            //if (GridManager.Instance.grids[i].card.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
            monsterCount++;
            GridPos grid = GridManager.Instance.grids[i];
            var swap = Instantiate(powerupPrefab, grid.card.transform);
            swap.transform.position = grid.card.transform.position;
            swap.pos = grid.pos;
            swap.card = grid.card;
            swap.buttonPowerup = this;
        }
        if (monsterCount == 0)
        {
            //Notify("OUT OF RANGE");
            hero.canMove = true;
        } else
        {
            TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        }
    }

    public override void CancelSkill()
    {
        base.CancelSkill();
        var powerUp = FindObjectsOfType<PowerUpLifeSteal>();
        for (int i = 0; i < powerUp.Length; i++)
        {
            Destroy(powerUp[i]);
        }
    }
}
