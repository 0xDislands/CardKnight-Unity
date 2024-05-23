using System.Collections;
using UnityEngine;

public class RevengeTag : MonsterTag
{
    public const float INCREASE_HP_RATIO = 1.2f;
    bool addEvent = false;

    public override IEnumerator IETurnEnd()
    {
        Debug.Log("Do nothing!");
        yield return null;
    }

    private void Start()
    {
        if (!addEvent && EventManager.Instance != null)
        {
            addEvent = true;
            EventManager.Instance.onMonsterDead += RevengeIncreaseHP;
        }
    }

    private void OnEnable()
    {
        if (!addEvent && EventManager.Instance != null)
        {
            addEvent = true;
            EventManager.Instance.onMonsterDead += RevengeIncreaseHP;
        }
    }

    private void OnDisable()
    {
        addEvent = false;
        if (EventManager.Instance != null)
        {
            EventManager.Instance.onMonsterDead -= RevengeIncreaseHP;
        }
    }

    public void RevengeIncreaseHP(Monster deadMonter)
    {
        Debug.Log("Revenge!!");
        monster.monsterData.maxHp = 0;
        float newHP = monster.monsterData.currentHp * INCREASE_HP_RATIO;

        monster.monsterData.currentHp = newHP;
        if (monster.monsterData.currentHp > monster.monsterData.maxHp)
        {
            monster.monsterData.maxHp = monster.monsterData.currentHp;
        }
        monster.SetHp(newHP);
        EffectManager.Instance.Heal(monster.transform.position);
    }
}