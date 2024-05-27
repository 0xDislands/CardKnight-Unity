using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilentTag : MonsterTag
{
    protected override void Awake()
    {
        type = TagType.Silient;
        base.Awake();
    }

    public override IEnumerator IETurnEnd()
    {
        yield return null;
    }
}
