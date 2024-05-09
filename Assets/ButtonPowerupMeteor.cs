using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ButtonPowerupMeteor : ButtonPowerup
{
    public MeteorObject meteor;
    public override void OnClick()
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return;
        if (!useable) return;
        CurrentAtkTime = 0;
        CardManager.Instance.StartCoroutine(IESpawnMeteor());
    }

    IEnumerator IESpawnMeteor()
    {
        var monsters = FindObjectsOfType<Monster>();
        var damgeValue = CardManager.Instance.hero.heroData.dame;
        foreach (var monster in monsters)
        {
            var newMeteor = SimpleObjectPool.Instance.GetObjectFromPool(meteor, monster.transform.position);
            newMeteor.FallToAttack(monster);
            newMeteor.damageData.damage = 1;
            yield return new WaitForSeconds(0.1f);
        }
    }
}