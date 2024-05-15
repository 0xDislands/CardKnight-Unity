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
        if (IsCooldownReady() == false) return;
        CurrentAtkTime = atkToAvailable;
        CardManager.Instance.StartCoroutine(IESpawnMeteor());
    }

    IEnumerator IESpawnMeteor()
    {
        var monsters = FindObjectsOfType<Monster>();
        var damgeValue = CardManager.Instance.hero.heroData.damage;
        foreach (var monster in monsters)
        {
            var newMeteor = SimpleObjectPool.Instance.GetObjectFromPool(meteor, monster.transform.position);
            newMeteor.damageData.damage = CardManager.Instance.hero.heroData.maxHp;
            newMeteor.FallToAttack(monster);
            yield return new WaitForSeconds(0.1f);
        }
    }
}