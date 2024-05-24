using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Dislands;
using System.Collections.Generic;

public class Monster : CardEffect
{
    public MonsterData monsterData;
    private TextHp textHp;
    [SerializeField] private ParticleSystem attackEffect;
    public MonsterTag[] tags;

    private void Awake()
    {
        textHp = GetComponentInChildren<TextHp>();
        tags = GetComponentsInChildren<MonsterTag>();
    }
    private void Start()
    {
        Init();
        monsterData.currentHp = monsterData.maxHp;
        textHp.SetHP(monsterData);
    }
    private void OnEnable()
    {
        Init();
    }

    public void SetTag(Dictionary<TagType, bool> dic)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            tags[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < tags.Length; i++)
        {
            tags[i].gameObject.SetActive(dic[tags[i].type]);
        }
    }
    public override void ApplyEffect(Hero hero)
    {
        var damage = new DamageData();
        damage.damage = (int)monsterData.currentHp;
        hero.TakeDamage(damage, out bool dead);
        if (dead == false)
        {
            hero.exp.AddEXP(monsterData.rewardExp);
            //Debug.Log($"add exp {monsterData.rewardExp}");
        }
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(attackEffect, transform.position);
        var card = GetComponent<Card>();
        card.Disappear();
        CardManager.Instance.MoveCardsAfterUse(card);
        EventManager.Instance.onMonsterDead?.Invoke(this);
    }

    public void Init()
    {
        var heroData = CardManager.Instance.hero.heroData;
        monsterData.maxHp = (int)((heroData.level + 1) * monsterData.baseHp * monsterData.multiple);
        monsterData.currentHp = monsterData.maxHp;
    }

    public void SetHp(float hp)
    {
        monsterData.currentHp = hp;
        if (monsterData.currentHp > monsterData.maxHp)
        {
            monsterData.currentHp = monsterData.maxHp;
        }
        textHp.SetHP(monsterData);
    }

    public void TakeDamage(DamageData data, out bool dead)
    {
        dead = false;
        monsterData.currentHp -= data.damage;
        if ((int)monsterData.currentHp <= 0)
        {
            monsterData.currentHp = 0;
            dead = true;
        }
        textHp.SetHP(monsterData);
        EffectManager.Instance.Hit(transform);

        if (dead == true)
        {
            CardManager.Instance.hero.exp.AddEXP(monsterData.rewardExp);
            Debug.Log($"add exp {monsterData.rewardExp}");

            var monsterCard = GetComponent<Card>();
            monsterCard.Disappear();
            var nextCardId = CardManager.Instance.GetNextCard();
            var newCard = CardManager.Instance.SpawnCard(monsterCard.Pos, nextCardId.cardId);
            newCard.ShowSpawnAnimation();
            if(newCard.TryGetComponent<Monster>(out var monster))
            {
                SetTag(nextCardId.tagDic);
            }
        }
    }
}