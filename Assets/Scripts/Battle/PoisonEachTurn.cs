﻿using System.Collections;
using UnityEngine;

public class PoisonEachTurn : TurnEndEffect
{
    public const int DEFAULT_TURN_COUNT = 3;
    public const float DELAY = 0.2f;
    public ParticleSystem poisonEffect;

    public int turnCount = DEFAULT_TURN_COUNT;
    private Hero hero;

    private void Awake()
    {
        hero = GetComponent<Hero>();
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
        turnCount--;
        if (!hero.hasMove) yield break;
        yield return new WaitForSeconds (DELAY);
        if (turnCount <= 0) Destroy(this);
        var damageData = new DamageData ();
        damageData.damage = 1;
        hero.TakeDamage(damageData, out bool dead);
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(poisonEffect, transform.position);
        effect.transform.SetParent(hero.transform);
    }
}