public class ButtonPowerupHeal : ButtonPowerup
{
    public override void OnClick()
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return;
        if (!useable) return;
        CurrentAtkTime = 0;
        CardManager.Instance.hero.AddHP(new DamageData(9999));
    }
}