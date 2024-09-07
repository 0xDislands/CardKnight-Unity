using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public const float ANIM_TIME = 0.5f;

    [SerializeField] private Image image;
    [SerializeField] private Sprite[] tutorialSprites;
    private int index;

    private void OnEnable()
    {
        transform.localScale = Vector3.one * 0.5f;
        transform.DOScale(1f, ANIM_TIME).SetEase(Ease.OutBack);
    }

    public void Next()
    {
        if (index < tutorialSprites.Length - 1) index++;
        
        image.DOFade(0f, 0.5f).OnComplete(() => 
        {
            image.sprite = tutorialSprites[index];
            image.DOFade(1f, 0.5f);
        });
    }
    public void Previous()
    {
        if (index > 0) index--;
        image.DOFade(0f, 0.5f).OnComplete(() =>
        {
            image.sprite = tutorialSprites[index];
            image.DOFade(1f, 0.5f);
        });
    }

    public void Close()
    {
        transform.DOScale(Vector3.one * 0.5f, ANIM_TIME).SetEase(Ease.InBack).OnComplete(() => 
        {
            gameObject.SetActive(false);
        });
    }    
}
