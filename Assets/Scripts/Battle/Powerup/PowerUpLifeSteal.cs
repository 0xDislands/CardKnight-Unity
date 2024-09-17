using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PowerUpLifeSteal : PowerupBase
{
    private void Awake()
    {
        id = PowerupId.Life_Steal;
    }
    public override void OnClick()
    {
        base.OnClick();
        var neighbour = CardManager.Instance.heroNeighbours;
        if (!neighbour.Contains(card.Pos))
        {
            var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            txtNotify.text = "Out of Range";
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Life_Steal);
            slashButton.CancelSkill();
            return;
        }

        if (IsImuned())
        {
            Gameplay.Instance.buttonPowerups.First(x => x.id == id).CancelSkill();
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextImune"), transform.position + new Vector3(0, 1f));
            return;
        }
        var monster = card.GetComponent<Monster>();
        if (monster)
        {
            monster.TakeDamage(new DamageData(monster.monsterData.maxHp * 0.2f), out var dead);
            CardManager.Instance.hero.Heal(new DamageData((Mathf.CeilToInt((monster.monsterData.maxHp * 0.2f)))));
        }
        else
        {
            var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            txtNotify.text = "Can only target monster";
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Life_Steal);
            slashButton.CancelSkill();
        }
        var powers = FindObjectsOfType<PowerUpLifeSteal>();
        for (int i = 0; i < powers.Length; i++)
        {
            powers[i].gameObject.SetActive(false);
        }
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}
