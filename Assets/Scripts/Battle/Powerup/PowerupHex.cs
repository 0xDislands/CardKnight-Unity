using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PowerupHex : PowerupBase
{
    private void Awake()
    {
        id = PowerupId.Hex;
    }
    public override void OnClick()
    {
        base.OnClick();
        var neighbour = CardManager.Instance.heroNeighbours;
        if (!neighbour.Contains(card.Pos))
        {
            var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            txtNotify.text = "Out of Range";
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.Hex);
            slashButton.CancelSkill();
            return;
        }
        if (IsImuned())
        {
            Gameplay.Instance.buttonPowerups.First(x => x.id == id).CancelSkill();
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextImune"), transform.position + new Vector3(0, 1f));
            return;
        }
        card.TryGetComponent<Monster>(out Monster monster);
        card.TryGetComponent<Boss>(out Boss boss);
        if(!monster && !boss)
        {
            var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            txtNotify.text = "Can only target monster";
            buttonPowerup.CancelSkill();
            return;
        }
        else if (monster != null && boss == null)
        {
            card.gameObject.SetActive(false);
            CardManager.Instance.RemoveCard(card);
            var newCard = CardManager.Instance.SpawnCard(card.Pos, CardId.HexedMonster);
            var hexedMonster = newCard.GetComponent<Monster>();
            hexedMonster.monsterData.rewardExp = monster.monsterData.rewardExp;
        }
        else if (boss != null)
        {
            var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshPro>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            txtNotify.text = "Boss";
            Gameplay.Instance.GetButtonPowerUpByID(PowerupId.Hex).GetComponent<ButtonPowerupHex>().CancelSkill();
        }

        var hexes = FindObjectsOfType<PowerupHex>();
        for (int i = 0; i < hexes.Length; i++)
        {
            Destroy(hexes[i]);
        }
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}