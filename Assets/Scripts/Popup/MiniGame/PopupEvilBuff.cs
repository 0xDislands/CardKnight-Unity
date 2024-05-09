using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class PopupEvilBuff : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;

    public Transform levelUpDataParent;
    public List<LevelUpOption> options;
    private CanvasGroup canvasGroup;
    private int index;
    private Card card;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowLevelUp(Card card)
    {
        this.card = card;
        canvasGroup.DOFade(1f, ANIMATION_TIME);
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);
        index++;

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
            }
            else
            {
                options[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnYes()
    {
        for (int i = 0; i < options.Count; i++)
        {
            options[i].OnClick();
        }
        Close();
    }

    public void Close()
    {
        canvasGroup.DOFade(0f, ANIMATION_TIME);
        transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(() => {
            gameObject.SetActive(false);
            CardManager.Instance.MoveCardsAfterUse(card);
            PopupManager.Instance.DoNextAction();
        });
    }
}