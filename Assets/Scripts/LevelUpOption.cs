using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpOption : MonoBehaviour
{
    public const int FULL_LIMIT = 9999;
    public Image imgDemo;
    public TextMeshProUGUI title;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI description;
    private ChangeStateData data;
    public bool changeColor;
    public Color colorGood;
    public Color colorBad;

    public void Show(ChangeStateData data)
    {
        this.data = data;
        imgDemo.sprite = data.sprite;
        title.text = data.title;
        description.text = data.description;
        if (data.amount >= FULL_LIMIT)
        {
            amount.text = "FULL";
        } else
        {
            amount.text = data.amount.ToString();
        }
        if (changeColor)
        {
            var color = data.type == LevelUpDataType.Bad ? colorBad : colorGood;
            title.color = color;
        }
    }

    public void OnClick()
    {
        switch (data.id)
        {
            case LevelUpId.ADD_HP:
                var damage = new DamageData();
                damage.damage = data.amount;
                CardManager.Instance.hero.AddHP(damage);
                break;
            case LevelUpId.ADD_AMOUR:
                CardManager.Instance.hero.AddShield(data.amount);
                break;
            case LevelUpId.INCREASE_MAX_HP:
                CardManager.Instance.hero.heroData.maxHp += data.amount;
                CardManager.Instance.hero.UpdateDisplay ();
                break;
            case LevelUpId.INCREASE_MAX_AMOUR:
                CardManager.Instance.hero.heroData.maxShield += data.amount;
                CardManager.Instance.hero.UpdateDisplay ();
                break;
            case LevelUpId.LOSE_CURRENT_HP_PERCENT:
                int hpLost = Mathf.FloorToInt(CardManager.Instance.hero.heroData.hp *((float)data.amount / 100));
                CardManager.Instance.hero.AddHP(new DamageData(-(int)hpLost));
                CardManager.Instance.hero.UpdateDisplay();
                break;
            case LevelUpId.INCREASE_EXP:
                CardManager.Instance.hero.AddEXP(data.amount);
                CardManager.Instance.hero.UpdateDisplay();
                break;
        }
    }
}