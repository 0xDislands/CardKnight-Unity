public class ButtonPowerupSlash : ButtonPowerup
{
    public PowerupSlash powerupPrefab;

    public override void OnClick()
    {
        if (active)
        {
            ResetSkill();
            return;
        }
        if (IsCooldownReady() == false) return;
        active = true;
        this.hero.canMove = false;
        CurrentAtkTime = atkToAvailable;

        var neightbours = CardManager.Instance.heroNeighbours;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            var slash = Instantiate(powerupPrefab, grid.card.transform);
            slash.transform.position = grid.card.transform.position;
            slash.pos = grid.pos;
            slash.card = grid.card;
            CardManager.Instance.UpdateHeroNeighbours();
        }
        Hero hero = CardManager.Instance.hero;
        hero.canMove = true;
    }
    public override void ResetSkill()
    {
        base.ResetSkill();
        var neightbours = CardManager.Instance.heroNeighbours;
        hero.canMove = true;
        for (int i = 0; i < neightbours.Count; i++)
        {
            GridPos grid = GridManager.Instance.dicGrids[neightbours[i]];
            var powerupSlash = grid.card.GetComponentInChildren<PowerupSlash>();
            if (powerupSlash) Destroy(powerupSlash.gameObject);
        }
    }
}