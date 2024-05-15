using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI toolTip;
    [SerializeField] private CanvasGroup group;

    private void OnEnable()
    {
        group.alpha = 0f;
        group.DOFade(1f, 0.2f);
    }

    public void DisplayToolTip(string content, Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        toolTip.text = content;
    }

    public void HideToolTip() => gameObject.SetActive(false);
}
