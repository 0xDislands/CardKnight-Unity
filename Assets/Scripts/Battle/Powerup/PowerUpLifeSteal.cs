using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (IsImuned())
        {
            Gameplay.Instance.buttonPowerups.First(x => x.id == id).ResetSkill();
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextImune"), transform.position + new Vector3(0, 1f));
            return;
        }
        var monster = card.GetComponent<Monster>();
        if (monster)
        {
            card.gameObject.SetActive(false);
            monster.TakeDamage(new DamageData(monster.monsterData.maxHp), out var dead);
            CardManager.Instance.hero.AddHP(new DamageData((int)(monster.monsterData.maxHp * 0.25f)));
        }
        else
        {
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Life_Steal);
            slashButton.ResetSkill();
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
