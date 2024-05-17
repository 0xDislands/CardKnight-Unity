using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;

public class PowerupSlash : PowerupBase
{
    public EffectSlash effect;
    public ParticleSystem attackEffect;
    private void Awake()
    {
        id = PowerupId.Slash;
    }
    public override void OnClick()
    {
        base.OnClick();
        if (IsImuned())
        {
            Gameplay.Instance.buttonPowerups.First(x => x.id == id).ResetSkill();
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextImune"), transform.position + new Vector3(0, 1f));
            return;
        }
        if (card.TryGetComponent<Monster>(out var monster))
        {
            var newEffect = SimpleObjectPool.Instance.GetObjectFromPool(effect, card.transform.position);
            newEffect.DoEffect();
            var newAttackEffect = SimpleObjectPool.Instance.GetObjectFromPool(attackEffect, card.transform.position);
            var damage = new DamageData();
            damage.damage = Mathf.RoundToInt(monster.monsterData.maxHp * 2.5f);
            monster.TakeDamage(damage, out bool dead);
        }
        else
        {
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Slash);
            slashButton.ResetSkill();
        }
        var slashes = FindObjectsOfType<PowerupSlash>();
        for (int i = 0; i < slashes.Length; i++)
        {
            slashes[i].gameObject.SetActive(false);
        }
        Hero hero = CardManager.Instance.hero;
        hero.canMove = true;
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}