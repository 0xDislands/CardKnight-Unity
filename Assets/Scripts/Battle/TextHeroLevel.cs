using UnityEngine;
using TMPro;

public class TextHeroLevel : MonoBehaviour
{
    private TextMeshProUGUI txtHeroLevel;
    private int level = -1;

    private void Awake()
    {
        txtHeroLevel = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (level != CardManager.Instance.hero.heroData.level)
        {
            level = CardManager.Instance.hero.heroData.level;
            txtHeroLevel.text = (level + 1).ToString();
        }
    }
}