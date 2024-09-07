using UnityEngine;

public class ButtonPowerupSlash : ButtonPowerup
{
    public PowerupSlash powerupPrefab;

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
        hero.canMove = false;
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        int monsterCount = 0;
        var heroCard = CardManager.Instance.heroCard;
        for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        {
            if (GridManager.Instance.grids[i].pos == heroCard.Pos) continue;
            GridPos grid = GridManager.Instance.grids[i];
            var curse = Instantiate(powerupPrefab, grid.card.transform);
            curse.transform.position = grid.card.transform.position;
            curse.pos = grid.pos;
            curse.card = grid.card;
            curse.buttonPowerup = this;
        }
        if (monsterCount == 0)
        {
            hero.canMove = true;
        } else
        {
            TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        }
    }
    public override void CancelSkill()
    {
        base.CancelSkill();
        var powerUp = FindObjectsOfType<PowerupSlash>();
        hero.canMove = true;
        for (int i = 0; i < powerUp.Length; i++)
        {
            if (powerUp[i]) Destroy(powerUp[i].gameObject);
        }
    }
}