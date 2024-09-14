using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextHp : MonoBehaviour
{
    private TextMeshProUGUI txtHp;

    private void Awake()
    {
        txtHp = GetComponent<TextMeshProUGUI>();
    }
    public void SetHpHero(HeroBattleData heroData)
    {
        txtHp.text = heroData.hp.ToString() + "/" + heroData.maxHp.ToString();
    }
    public void SetHpMonster(MonsterData monsterData)
    {
        txtHp.text = (Mathf.FloorToInt(monsterData.currentHp)).ToString();
    }
}