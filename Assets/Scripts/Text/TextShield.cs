using UnityEngine;
using TMPro;

public class TextShield : MonoBehaviour
{
    private TextMeshProUGUI txtHp;

    private void Awake()
    {
        txtHp = GetComponent<TextMeshProUGUI>();
    }
    public void SetShield(HeroBattleData heroData)
    {
        txtHp.text = Mathf.Round(heroData.shield) + "/" + Mathf.Round(heroData.maxShield);
    }
}