using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoHopeTag : MonsterTag
{
    protected override void Awake()
    {
        type = TagType.NoHope;
        base.Awake();
    }

    public override IEnumerator IETurnEnd()
    {
        yield break;
    }
}
