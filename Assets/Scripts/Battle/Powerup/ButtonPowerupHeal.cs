using UnityEngine;
using UnityEngine.UI;

public class ButtonPowerupHeal : ButtonPowerup
{
    [SerializeField] private Image disableImg;
    [SerializeField] private Button button;

    private void Start()
    {
        EventManager.Instance.onNewCardSpawned += CheckSkill;
        EventManager.Instance.onCardDisappeared += CheckSkill;
    }

    public override void OnClick()
    {
        if (SkillDisable())
        {
            var text = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextFlyUpFade>("TextOnCooldown"), transform.position + new Vector3(0, 1f));
            text.text.text = "Silent";
            return;
        }
        if (IsLevelReady() == false)
        {
            Notify("LEVEL UP TO UNLOCK");
            return;
        }
        if (IsCooldownReady() == false)
        {
            Notify("ON COOLDOWN");
            return;
        }
        TurnLeftToUSeSkill = maxTurnLeftToUseSkill;
        CardManager.Instance.hero.Heal(new DamageData(9999));
    }

    private void CheckSkill(Card card)
    {
        if (!IsLevelReady()) return;
        var noHeal = CardManager.Instance.FindTag(TagType.NoHope);
        if(noHeal == null)
        {
            button.interactable = true;
            disableImg.gameObject.SetActive(false);
        }
        else
        {
            button.interactable = false;
            disableImg.gameObject.SetActive(true);
        }
    }
}