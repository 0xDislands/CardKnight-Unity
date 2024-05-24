using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

        var neighbour = CardManager.Instance.heroNeighbours;
        if(!neighbour.Contains(card.Pos))
        {
            var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            txtNotify.text = "Out of Range";
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Curse);
            slashButton.CancelSkill();
            return;
        }

        if (IsImuned())
        {
            Gameplay.Instance.buttonPowerups.First(x => x.id == id).CancelSkill();
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextImune"), transform.position + new Vector3(0, 1f));
            return;
        }
        var heroCard = CardManager.Instance.heroCard;
        card.transform.SetAsLastSibling();
        heroCard.transform.SetAsLastSibling();

        var monster = card.GetComponent<Monster>();

        if (monster != null)
        {
            monster.TakeDamage(new DamageData(monster.monsterData.currentHp /2f), out var dead);
        } else
        {
            var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            txtNotify.text = "Can only target monster";
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Curse);
            slashButton.CancelSkill();
        }
        var swaps = FindObjectsOfType<PowerupCurse>();
        for (int i = 0; i < swaps.Length; i++)
        {
            swaps[i].gameObject.SetActive(false);
        }
        Hero hero = CardManager.Instance.hero;
        hero.canMove = true;
        EffectManager.Instance.Hit(card.transform);
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}