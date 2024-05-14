using System.Collections.Generic;
using UnityEngine;
public class ButtonPowerupHex : ButtonPowerup
{
    public PowerupHex powerupPrefab;
    public override void OnClick()
    {
        var neightbours = CardManager.Instance.heroNeighbours;
        if (currentAtkTime == 0)
        {
            ResetSkill();
            return;
        }
        if (IsCooldownReady() == false) return;
        hero.canMove = false;
        int monsterCount = 0;

        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            if (!grid.card.TryGetComponent<Monster>(out Monster monster)) continue;
            monsterCount++;
            var slash = Instantiate(powerupPrefab, grid.card.transform);
            slash.transform.position = grid.card.transform.position;
            slash.pos = grid.pos;
            slash.card = grid.card;
            CardManager.Instance.UpdateHeroNeighbours();
        }
        if (monsterCount == 0)
        {
            Debug.Log("monster not found");
            hero.canMove = true;
        } else
        {
            CurrentAtkTime = 0;
        }
    }
    public void ResetSkill()
    {
        var neightbours = CardManager.Instance.heroNeighbours;
        CurrentAtkTime = DataManager.Instance.dicPowerUp[id].cooldown;
        hero.canMove = true;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            PowerupHex powerupHex = grid.card.GetComponentInChildren<PowerupHex>();
            if (powerupHex) Destroy(powerupHex.gameObject);
        }
    }
}