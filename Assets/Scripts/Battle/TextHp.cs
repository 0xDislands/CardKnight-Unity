using UnityEngine;
using TMPro;

public class TextHp : MonoBehaviour
{
    private TextMeshProUGUI txtHp;

    private void Awake()
    {
        txtHp = GetComponent<TextMeshProUGUI>();
    }
    public void SetHP(int newHp)
    {
        txtHp.text = newHp.ToString();
    }
}