using UnityEngine;

public class ButtonPowerupHeal : ButtonPowerup
{
    public override void OnClick()
    {
        if (IsCooldownReady() == false) return;
        CurrentAtkTime = 0;
        CardManager.Instance.hero.AddHP(new DamageData(9999));
    }
}