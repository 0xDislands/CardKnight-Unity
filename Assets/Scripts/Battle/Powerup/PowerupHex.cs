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
