using DG.Tweening;
using UnityEngine;

public class MeteorObject : MonoBehaviour
{
    public const float SPAWN_X = 4f;
    public const float SPAWN_Y = 8f;
    public const float FALL_TIME = 0.35f;
    public ParticleSystem touchGroundEffect;
    public DamageData damageData;

    public void FallToAttack(Monster monster)
    {
        var card = monster.GetComponent<Card>();
        var grid = GridManager.Instance.dicGrids[card.Pos];
        transform.position = grid.transform.position + new Vector3(SPAWN_X, SPAWN_Y);
        transform.DOMove(grid.transform.position, FALL_TIME).SetEase(Ease.Linear).OnComplete(() =>
        {
            //SimpleObjectPool.Instance.GetObjectFromPool(touchGroundEffect, transform.position);
            monster.TakeDamage(damageData, out var dead);
            gameObject.SetActive(false);
        });
    }
}