using System.Collections;
using UnityEngine;

public class RevengeTag : MonsterTag
{
    public const float INCREASE_HP_RATIO = 0.2f;
    bool addEvent = false;

    protected override void Awake()
    {
        type = TagType.Revenge;
        base.Awake();
    }

    private void Start()
    {
        if (!addEvent && EventManager.Instance != null)
        {
            addEvent = true;
            EventManager.Instance.onMonsterDead += RevengeIncreaseHP;
        }
    }


    public override IEnumerator IETurnEnd()
    {
        Debug.Log("Do nothing!");
        yield return null;
    }


    //private void OnDisable()
    //{
    //    addEvent = false;
    //    if (EventManager.Instance != null)
    //    {
    //        EventManager.Instance.onMonsterDead -= RevengeIncreaseHP;
    //    }
    //}

    public void RevengeIncreaseHP(Monster deadMonter)
    {
        if (gameObject.activeInHierarchy == false) return;

        Debug.Log("Revenge!!");
        monster.monsterData.maxHp = 0;
        float increaseHp = monster.monsterData.currentHp * INCREASE_HP_RATIO;
        if (increaseHp < 1) increaseHp = 1f;

        monster.monsterData.currentHp += increaseHp;
        if (monster.monsterData.currentHp > monster.monsterData.maxHp)
        {
            monster.monsterData.maxHp = monster.monsterData.currentHp;
        }
        monster.SetHp(monster.monsterData.currentHp);
        EffectManager.Instance.Heal(monster.transform);
    }
}