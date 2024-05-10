using UnityEngine;

public class PowerupUnfairTrade : MonoBehaviour
{
    public Vector2Int pos;
    public Card card;
    public void OnClick()
    {
        var heroCard = CardManager.Instance.heroCard;
        card.transform.SetAsLastSibling();
        heroCard.transform.SetAsLastSibling();

        int heroHp = CardManager.Instance.hero.heroData.hp;
        var monster = card.GetComponent<Monster>();
        int monsterHp = monster.monsterData.currentHp;

        if (monster != null)
        {
            CardManager.Instance.hero.SetHP(monsterHp);
            monster.SetHp(heroHp);
        }
        var swaps = FindObjectsOfType<PowerupUnfairTrade>();
        for (int i = 0; i < swaps.Length; i++)
        {
            swaps[i].gameObject.SetActive(false);
        }
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}