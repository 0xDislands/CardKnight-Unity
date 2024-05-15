using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthTag : MonsterTag
{
    public override IEnumerator IETurnEnd()
    {
        var addHp = monster.monsterData.maxHp * 0.1f;
        monster.monsterData.maxHp += addHp;
        monster.monsterData.currentHp += addHp;
        monster.SetHp(monster.monsterData.currentHp);
        yield return null;
    }
}
