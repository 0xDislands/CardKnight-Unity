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
    public Button button;
    public bool changeColor;
    public Color colorGood;
    public Color colorBad;

    public void Show(ChangeStateData data)
    {
        if (button == null) button = GetComponent<Button>();
        var tag = CardManager.Instance.FindTag(TagType.NoHope);
        if (tag != null && data.id == LevelUpId.ADD_HP)
        {
            button.image.color = new Color(1, 1, 1, 0.5f);
            imgDemo.color = new Color(1, 1, 1, 0.5f);
        } else
        {
            button.image.color = new Color(1, 1, 1, 1f);
            imgDemo.color = Color.white;
        }
        this.data = data;
        imgDemo.sprite = data.sprite;
        title.text = data.Title;
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
        var levelUpPopup = GetComponentInParent<PopupLevelUp>();
        switch (data.id)
        {
            case LevelUpId.ADD_HP:
                var tag = CardManager.Instance.FindTag(TagType.NoHope);
                if (tag != null)
                {
                    levelUpPopup.Warninng(transform.position);
                    return;
                }
                var damage = new DamageData();
                damage.damage = data.amount;
                CardManager.Instance.hero.Heal(damage);
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
                int hpLost = Mathf.FloorToInt(CardManager.Instance.hero.heroData.hp *0.5f);
                CardManager.Instance.hero.Heal(new DamageData(-(int)hpLost));
                CardManager.Instance.hero.UpdateDisplay();
                break;
            case LevelUpId.INCREASE_EXP:
                CardManager.Instance.hero.exp.AddEXP(data.amount);
                CardManager.Instance.hero.UpdateDisplay();
                break;
        }
        levelUpPopup.Close();
    }
}