using UnityEngine;
using DG.Tweening;
using System.Collections;

public class PopupSelectHero : MonoBehaviour
{
    public const float ANIMATION_TIME = 0.5f;
    public const float RUN_TEXT_TIME = 2f;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        StartCoroutine(IEDoEffect());
    }

    public IEnumerator IEDoEffect()
    {
        canvasGroup.DOFade(1f, ANIMATION_TIME);
        gameObject.SetActive(true);
        transform.localScale = Vector3.one * 0.8f;
        transform.DOScale(1f, ANIMATION_TIME).SetEase(Ease.OutBack);
        yield return null;
    }

    public void Close()
    {
        canvasGroup.DOFade(0f, ANIMATION_TIME);
        transform.DOScale(0f, ANIMATION_TIME).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
            PopupManager.Instance.DoNextAction();
        });
    }
}