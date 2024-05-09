using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMeteor : ButtonPowerup
{
    public override void OnClick()
    {
        var monsters = FindObjectsOfType<Monster>();
        var damgeValue = CardManager.Instance.hero.heroData.dame;
        foreach (var monster in monsters)
        {
            monster.TakeDamage(new DamageData(damgeValue), out var dead);
        }
    }
}
