using UnityEngine;

public class ButtonPowerupHeal : ButtonPowerup
{
    public override void OnClick()
    {
        if (IsCooldownReady() == false) 
        {
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            return;
        }
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        CardManager.Instance.hero.Heal(new DamageData(9999));
    }
}