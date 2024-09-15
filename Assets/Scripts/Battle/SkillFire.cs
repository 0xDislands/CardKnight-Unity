﻿using UnityEngine;
using Dislands;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class SkillFire : Skill
{
    public const int ATTACK_NUMBER = 2;

    public ParticleSystem fireEffect;
    public ParticleSystem fireEffectExplosive;
    public override void ApplyEffect (Hero hero)
    {
        var monsters = GetAllMonsters ();
        monsters.Shuffle ();
        if (monsters.Count > ATTACK_NUMBER)
        {
            monsters = monsters.GetRange (0, ATTACK_NUMBER);
        }

        CardManager.Instance.StartCoroutine (IEUseSkill (monsters));

        var card = GetComponent<Card> ();
        card.Disappear ();
        CardManager.Instance.MoveCardsAfterUse(card);
    }

    IEnumerator IEUseSkill (List<Monster> monsters)
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            var effect = SimpleObjectPool.Instance.GetObjectFromPool (fireEffect, transform.position);
            var monster = monsters[i];
            effect.transform.DOMove (monsters[i].transform.position, 0.5f).SetEase (Ease.OutCubic).OnComplete (() =>
            {
                var damageData = new DamageData ();
                damageData.damage = 1;
                monster.TakeDamage (damageData, out bool dead);
                var effect2 = SimpleObjectPool.Instance.GetObjectFromPool(fireEffectExplosive, monster.transform.position);
            });
            yield return new WaitForSeconds (0.2f);
        }
    }

    private List<Monster> GetAllMonsters ()
    {
        var monsters = new List<Monster> ();
        for (int i = 0; i < CardManager.Instance.cards.Count; i++)
        {
            if (CardManager.Instance.cards[i] == null) continue;
            if (CardManager.Instance.cards[i].gameObject.activeInHierarchy == false) continue;

            var monster = CardManager.Instance.cards[i].GetComponent<Monster> ();
            if (monster != null) monsters.Add (monster);
        }
        return monsters;
    }
}