using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUpOption : MonoBehaviour
{
    public const int FULL_LIMIT = 9999;
    public Image imgDemo;
    public TextMeshProUGUI title;
    public TextMeshProUGUI amount;
    public TextMeshProUGUI description;
    public Image highLight;
    public ChangeStateData data;
    public Button button;
    public bool changeColor;
    public Color colorGood;
    public Color colorBad;

    private void OnEnable()
    {
        if (button) button.interactable = true;
        highLight?.gameObject.SetActive(false);
    }

    public void Show(ChangeStateData data)
    {
        if (button == null) button = GetComponent<Button>();
        var tag = CardManager.Instance.FindTag(TagType.NoHope);
        if (tag != null && data.id == LevelUpId.ADD_HP)
        {
            if (button) button.image.color = new Color(1, 1, 1, 0.5f);
            imgDemo.color = new Color(1, 1, 1, 0.5f);
        } else
        {
            if (button) button.image.color = new Color(1, 1, 1, 1f);
            imgDemo.color = Color.white;
        }
        this.data = data;
        imgDemo.sprite = data.sprite;
        title.text = data.Title;
        description.text = "";
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
        Notify(title.text);
        highLight?.gameObject.SetActive(true);
        var levelUpPopup = GetComponentInParent<PopupLevelUp>();
        if (levelUpPopup != null)
        {
            foreach (var levelUp in levelUpPopup.options)
            {
                if (levelUp.button) levelUp.button.interactable = false;
            }
        }
        switch (data.id)
        {
            case LevelUpId.ADD_HP:
                var tag = CardManager.Instance.FindTag(TagType.NoHope);
                if (tag != null)
                {
                    if (levelUpPopup) levelUpPopup.Warninng(transform.position);
                    return;
                }
                var damage = new DamageData();
                damage.damage = data.amount;
                CardManager.Instance.hero.Heal(damage);
                break;
            case LevelUpId.ADD_AMOUR:
                CardManager.Instance.hero.AddShield(data.amount);
                break;
            case LevelUpId.INCREASE_MAX_HP_PERCENT:
                float newValue = CardManager.Instance.hero.heroData.maxHp * (((float)data.amount) / 100f);
                CardManager.Instance.hero.heroData.maxHp += Mathf.RoundToInt(newValue);
                CardManager.Instance.hero.UpdateDisplay ();
                break;
            case LevelUpId.INCREASE_MAX_AMOUR_PERCENT:
                float val2 = CardManager.Instance.hero.heroData.maxShield * (((float)data.amount) / 100f);
                CardManager.Instance.hero.heroData.maxShield += Mathf.RoundToInt(val2);
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
            case LevelUpId.ADD_HP_PERCENT:
                int healPercent = Mathf.RoundToInt(CardManager.Instance.hero.heroData.maxHp * (((float)data.amount) / 100f));
                CardManager.Instance.hero.Heal(new DamageData((int)healPercent));
                CardManager.Instance.hero.UpdateDisplay();
                break;
            case LevelUpId.LOSE_POINT_PERCENT:
                Gameplay.Instance.Score *= 0.9f;
                break;
            case LevelUpId.RESET_COOL_DOWN_ALL_SKILL:
                var buttons = FindObjectsOfType<ButtonPowerup>();
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].CancelSkill();
                }
                break;
        }
        LeanTweenExt.LeanDelayedCall(gameObject, 0.5f, () =>
        {
            if (levelUpPopup) levelUpPopup.Close();
        });
    }

    public void Notify(string text)
    {
        var pos = Camera.main.ScreenToWorldPoint(transform.position);
        var popup = GameObject.FindGameObjectWithTag("CanvasPopup");
        var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshProUGUI>("TextFlyingUGUI"), popup.transform);

        txtNotify.transform.SetParent(popup.transform, false);
        txtNotify.text = text;
    }
}