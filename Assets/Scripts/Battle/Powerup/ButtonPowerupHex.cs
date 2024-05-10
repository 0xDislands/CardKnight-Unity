public class ButtonPowerupHex : ButtonPowerup
{
    public PowerupHex powerupPrefab;

    public override void OnClick()
    {
        var unlockLevel = DataManager.Instance.dicPowerUp[id].unlockLevel;
        if (CardManager.Instance.hero.heroData.level < unlockLevel) return;
        if (!useable) return;
        CurrentAtkTime = 0;


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
        //var hero = CardManager.Instance.heroCard;
        //for (int i = 0; i < GridManager.Instance.grids.Length; i++)
        //{
        //    if (GridManager.Instance.grids[i].pos == hero.Pos) continue;
        //    GridPos grid = GridManager.Instance.grids[i];
        //    var swap = Instantiate(powerupPrefab, grid.card.transform);
        //    swap.transform.position = grid.card.transform.position;
        //    swap.pos = grid.pos;
        //    swap.card = grid.card;
        //}
    }
}