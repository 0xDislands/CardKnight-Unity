using System.Linq;
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

        if (monster != null)
        {
            int monsterHp = (int)monster.monsterData.currentHp;
            CardManager.Instance.hero.SetHP(monsterHp);
            monster.SetHp(heroHp);
        } else
        {
            var slashButton = Gameplay.Instance.buttonPowerups.FirstOrDefault(x => x.id == PowerupId.UnfairTrade);
            slashButton.ResetSkill();
        }
        var swaps = FindObjectsOfType<PowerupUnfairTrade>();
        for (int i = 0; i < swaps.Length; i++)
        {
            swaps[i].gameObject.SetActive(false);
        }
        Hero hero = CardManager.Instance.hero;
        hero.canMove = true;
    }
    public void OnDisable()
    {
        Destroy(gameObject);
    }
}