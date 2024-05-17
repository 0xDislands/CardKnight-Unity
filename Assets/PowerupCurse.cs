using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerupCurse : PowerupBase
{
    private void Awake()
    {
        id = PowerupId.Curse;
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
        EffectManager.Instance.Hit(card.transform.position);
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}