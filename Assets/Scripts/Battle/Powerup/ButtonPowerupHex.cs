using UnityEngine;
public class ButtonPowerupHex : ButtonPowerup
{
    public PowerupHex powerupPrefab;
    public override void OnClick()
    {
        var neightbours = CardManager.Instance.heroNeighbours;
        if (active)
        {
            ResetSkill();
            return;
        }
        if (IsCooldownReady() == false) return;
        active = true;
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
            CurrentAtkTime = atkToAvailable;
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