using UnityEngine;

public class ButtonPowerupHeal : ButtonPowerup
{
    public override void OnClick()
    {
        if (SkillDisable())
        {
            var text = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            text.text.text = "Silent";
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
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        CardManager.Instance.hero.Heal(new DamageData(9999));
    }
}