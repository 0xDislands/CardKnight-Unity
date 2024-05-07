public class ButtonPowerupHeal : ButtonPowerup
{
    public override void OnClick()
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return;
        CardManager.Instance.hero.AddHP(new DamageData(9999));
    }
}