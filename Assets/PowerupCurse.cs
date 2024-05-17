using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerupCurse : PowerupBase
{
    public void OnClick()
    {
        var heroCard = CardManager.Instance.heroCard;
        card.transform.SetAsLastSibling();
        heroCard.transform.SetAsLastSibling();

        var monster = card.GetComponent<Monster>();

        if (monster != null)
        {
            monster.SetHp(monster.monsterData.currentHp /2f);
        } else
        {
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Curse);
            slashButton.ResetSkill();
        }
        var swaps = FindObjectsOfType<PowerupCurse>();
        for (int i = 0; i < swaps.Length; i++)
        {
            swaps[i].gameObject.SetActive(false);
        }
        Hero hero = CardManager.Instance.hero;
        hero.canMove = true;
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}