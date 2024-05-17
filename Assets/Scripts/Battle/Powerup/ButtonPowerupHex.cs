using UnityEngine;
public class ButtonPowerupHex : ButtonPowerup
{
    public PowerupHex powerupPrefab;
    public override void OnClick()
    {
        var neightbours = CardManager.Instance.heroNeighbours;
        if (isUsingSkill)
        {
            ResetSkill();
            return;
        }
        if (IsCooldownReady() == false)
        {
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            return;
        }
        isUsingSkill = true;
        hero.canMove = false;   
        int monsterCount = 0;

        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            if (grid.card.GetComponentInChildren<ImueMagicTag>() != null) continue;
            if (!grid.card.TryGetComponent<Monster>(out var monster)) continue;
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
            TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        }
    }
    public override void ResetSkill()
    {
        base.ResetSkill();
        var neightbours = CardManager.Instance.heroNeighbours;
        hero.canMove = true;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            PowerupHex powerupHex = grid.card.GetComponentInChildren<PowerupHex>();
            if (powerupHex) Destroy(powerupHex.gameObject);
        }
    }
}