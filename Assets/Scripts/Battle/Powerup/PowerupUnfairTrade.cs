using System.Linq;
using UnityEngine;

public class PowerupUnfairTrade : PowerupBase
{
    private void Awake()
    {
        id = PowerupId.UnfairTrade;
    }
    public override void OnClick()
    {
        base.OnClick();
        if (IsImuned())
        {
            Gameplay.Instance.buttonPowerups.First(x => x.id == id).CancelSkill();
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextImune"), transform.position + new Vector3(0, 1f));
            return;
        }
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
            slashButton.CancelSkill();
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