using UnityEngine;

public class PowerupHex : MonoBehaviour
{
    public const float MOVE_TIME = 0.5f;
    public Vector2Int pos;
    public Card card;
    public void OnClick()
    {
        var monster = card.GetComponent<Monster>();
        if (monster)
        {
            card.gameObject.SetActive(false);
            var newCard = CardManager.Instance.SpawnCard(card.Pos, CardId.HexedMonster);
            var hexedMonster = newCard.GetComponent<Monster>();
            hexedMonster.monsterData.rewardExp = monster.monsterData.rewardExp;
        }
        //var heroCard = CardManager.Instance.heroCard;
        //card.transform.SetAsLastSibling();
        //heroCard.transform.SetAsLastSibling();

        //var grid = GridManager.Instance.dicGrids[card.Pos];
        //var heroGrid = GridManager.Instance.dicGrids[heroCard.Pos];

        //heroCard.transform.DOMove(grid.transform.position, MOVE_TIME);
        //card.transform.DOMove(heroGrid.transform.position, MOVE_TIME);
        //card.Pos = heroGrid.pos;
        //heroCard.Pos = grid.pos;


        //CardManager.Instance.UpdateHeroNeighbours();
        var hexes = FindObjectsOfType<PowerupHex>();
        for (int i = 0; i < hexes.Length; i++)
        {
            hexes[i].gameObject.SetActive(false);
        }
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}
