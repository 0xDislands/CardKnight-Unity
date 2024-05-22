using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagInfo : MonoBehaviour
{
    private TagDisplay[] tagDisplays;

    private void Awake()
    {
        tagDisplays= GetComponentsInChildren<TagDisplay>();
    }

    public void DisplayTags(MonsterTag[] tags)
    {
        var tagDic = DataManager.Instance.dicTag;
        foreach (var display in tagDisplays)
        {
            display.group.alpha = 0f;
        }
        for (int i = 0; i < tags.Length; i++)
        {
            if (!tags[i].gameObject.activeInHierarchy) continue;
            var data = tagDic[tags[i].type];
            tagDisplays[i].group.alpha = 1f;
            tagDisplays[i].Display(data.sprite, data.description);
        }
    }
}
