using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpLifeSteal : PowerupBase
{
    public override void OnClick()
    {
        base.OnClick();
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
