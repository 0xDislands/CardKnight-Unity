using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PopupEvilBuff : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;

    public Transform levelUpDataParent;
    public List<LevelUpOption> options;
    private CanvasGroup canvasGroup;
    private int index;
    private Card card;
    private Button[] buttons;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        buttons = GetComponentsInChildren<Button>(true);
    }

    public void ShowLevelUp(Card card)
    {
        this.card = card;
        canvasGroup.DOFade(1f, ANIMATION_TIME);
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);
        index = Random.Range(0, levelUpDataParent.transform.childCount);

        if (index >= levelUpDataParent.transform.childCount)
        {
            Debug.LogWarning("not enough data to show: " + index);
            index = 0;
        }
        var step = levelUpDataParent.GetChild(index);
        for (int i = 0; i < options.Count; i++)
        {
            if (i < step.childCount)
            {
                var levelUpData = step.GetChild(i).GetComponent<ChangeStateData>();
                options[i].Show(levelUpData);
                SetIntertracableButton(true);
            }
            else
            {
                options[i].gameObject.SetActive(false);
            }
        }
    }
    void SetIntertracableButton(bool intertracable)
    {
        foreach (var button in buttons)
        {
            button.interactable = intertracable;
        }
    }
    public void OnYes()
    {
        for (int i = 0; i < options.Count; i++)
        {
            var option = options[i];
            DOVirtual.DelayedCall(i * 1f, () =>
            {
                option.OnClick();
            });    
        }
        SetIntertracableButton(false);
        canvasGroup.DOFade(0f, ANIMATION_TIME);
        transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(() => {
            gameObject.SetActive(false);
            card.Disappear();
            CardManager.Instance.MoveCardsAfterUse(card);
            PopupManager.Instance.DoNextAction();
        });
    }

    public void Close()
    {
        Notify("Cancel Evil Buff");
        SetIntertracableButton(false);
        canvasGroup.DOFade(0f, ANIMATION_TIME);
        transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(() => {
            gameObject.SetActive(false);
            card.Disappear();
            CardManager.Instance.MoveCardsAfterUse(card);
            PopupManager.Instance.DoNextAction();
        });
    }

    public void Notify(string text)
    { 
        var popup = GameObject.FindGameObjectWithTag("CanvasPopup");
        var txtNotify = SimpleObjectPool.Instance.GetObjectFromPool(Resources.Load<TextMeshProUGUI>("TextFlyingUGUI"), popup.transform);
        txtNotify.transform.SetParent(popup.transform, false);
        txtNotify.text = text;
    }
}