using UnityEngine;

public class ButtonPowerupUnfairTrade : ButtonPowerup
{
    public PowerupUnfairTrade powerupPrefab;

    public override void OnClick()
    {
        if (SkillDisable())
        {
            var text = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            text.text.text = "Silent";
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
        int monsterCount = 0;

        var heroCard = CardManager.Instance.heroCard;
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            if (GridManager.Instance.grids[i].pos == heroCard.Pos) continue;
            //if (GridManager.Instance.grids[i].card.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
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
            this.hero.canMove = true;
        } else
        {
            TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        }
    }
    public override void CancelSkill()
    {
        base.CancelSkill();
        var powerUp = FindObjectsOfType<PowerupUnfairTrade>();
        for (int i = 0; i < powerUp.Length; i++)
        {
            Destroy(powerUp[i]);
        }
    }
}