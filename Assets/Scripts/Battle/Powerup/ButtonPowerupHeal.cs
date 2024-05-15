using UnityEngine;

public class ButtonPowerupHeal : ButtonPowerup
{
    public override void OnClick()
    {
        if (IsCooldownReady() == false) return;
        CurrentAtkTime = atkToAvailable;
        CardManager.Instance.hero.AddHP(new DamageData(9999));
    }
}