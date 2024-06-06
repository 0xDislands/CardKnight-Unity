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
    public void SetHP(HeroBattleData heroData)
    {
        txtHp.text = heroData.hp.ToString();
    }
    public void SetHP(MonsterData monsterData)
    {
        txtHp.text = (Mathf.FloorToInt(monsterData.currentHp)).ToString();
    }
}