using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmuneMagicTag : MonsterTag
{
    protected override void Awake()
    {
        type = TagType.NoMagic;
        base.Awake();
    }

    public override IEnumerator IETurnEnd()
    {
        yield break;
    }
}
