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
        CardManager.Instance.StartCoroutine(IESpawnMeteor());
    }

    IEnumerator IESpawnMeteor()
    {
        var monsters = FindObjectsOfType<Monster>();
        var damgeValue = hero.heroData.damage;
        foreach (var monster in monsters)
        {
            if (monster.GetComponentInChildren<ImmuneMagicTag>() != null) continue;
            var newMeteor = SimpleObjectPool.Instance.GetObjectFromPool(meteor, monster.transform.position);
            float damage = Mathf.CeilToInt(hero.heroData.hp * 0.25f);
            if (damage < 1) damage = 1;
            newMeteor.damageData.damage = damage;
            newMeteor.FallToAttack(monster);
            yield return new WaitForSeconds(0.1f);
        }
    }
}