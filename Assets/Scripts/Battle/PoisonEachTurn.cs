using System.Collections;
using UnityEngine;

public class PoisonEachTurn : TurnEndEffect
{
    public const int DEFAULT_TURN_COUNT = 4;
    public const float DELAY = 0.2f;
    public ParticleSystem poisonEffect;

    public int turnCount = DEFAULT_TURN_COUNT;
    //public int damage;
    private Hero hero;
    private DamageData damageData;
    private void Awake()
    {
        hero = GetComponent<Hero>();
        //damageData = new DamageData();
        //damageData.damage = damage;
    }
    private void OnEnable()
    {
        ResetTurnCount();
    }
    public void ResetTurnCount()
    {
        turnCount = DEFAULT_TURN_COUNT;
    }
    public override IEnumerator IETurnEnd()
    {
        yield return new WaitForSeconds (DELAY);
        turnCount--;
        if (turnCount < 0) Destroy(this);
        var damageData = new DamageData ();
        damageData.damage = 1;
        hero.TakeDamage(damageData, out bool dead);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(poisonEffect, transform.position);
        effect.transform.SetParent(hero.transform);
        Debug.Log($"spawn poison at game object: " + gameObject.name);
    }
}