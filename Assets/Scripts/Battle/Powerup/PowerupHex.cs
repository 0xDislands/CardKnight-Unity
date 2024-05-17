using System.Collections.Generic;
using UnityEngine;

public class PowerupHex : PowerupBase
{
    public override void OnClick()
    {
        base.OnClick();
        card.TryGetComponent<Monster>(out Monster monster);
        monster.TryGetComponent<Boss>(out Boss boss);
        if (monster != null && boss == null)
        {
            card.gameObject.SetActive(false);
            CardManager.Instance.RemoveCard(card);
            var newCard = CardManager.Instance.SpawnCard(card.Pos, CardId.HexedMonster);
            var hexedMonster = newCard.GetComponent<Monster>();
            hexedMonster.monsterData.rewardExp = monster.monsterData.rewardExp;
        }
        else if (boss != null)
        {
            Gameplay.Instance.GetButtonPowerUpByID(PowerupId.Hex).GetComponent<ButtonPowerupHex>().ResetSkill();
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