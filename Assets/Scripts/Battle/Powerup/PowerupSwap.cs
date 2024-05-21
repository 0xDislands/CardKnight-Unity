using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PowerupSwap : PowerupBase
{
    private void Awake()
    {
        id = PowerupId.Swap;
    }

    public override void OnClick()
    {
        base.OnClick();
        var heroCard = CardManager.Instance.heroCard;
        card.transform.SetAsLastSibling();
        heroCard.transform.SetAsLastSibling();
        if(IsImuned())
        {
            Gameplay.Instance.buttonPowerups.First(x => x.id == id).CancelSkill();
            SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextImune"), transform.position + new Vector3(0, 1f));
            return;
        }
        var grid = GridManager.Instance.dicGrids[card.Pos];
        var heroGrid = GridManager.Instance.dicGrids[heroCard.Pos];

        heroCard.transform.DOMove(grid.transform.position, MOVE_TIME);
        card.transform.DOMove(heroGrid.transform.position, MOVE_TIME);
        card.Pos = heroGrid.pos;
        heroCard.Pos = grid.pos;
        CardManager.Instance.UpdateHeroNeighbours();
        var swaps = FindObjectsOfType<PowerupSwap>();
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
