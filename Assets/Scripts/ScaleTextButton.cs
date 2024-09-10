using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScaleTextButton : MonoBehaviour
{
    public Vector3 scale;
    public float scaleDuration;
    public Vector3 movePosition;
    public float moveDuration;

    private Vector3 startPosition;
    private Vector3 startScale;
    private TextMeshProUGUI tmpro;
    private void Awake()
    {
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
        startPosition = tmpro.transform.localPosition;
        startScale = tmpro.transform.localScale;
    }
    public void ButtonMouseDown()
    {
        tmpro.transform.DOScale(startScale + scale, scaleDuration);
        tmpro.transform.DOLocalMove(startPosition + movePosition * 0.5f, moveDuration);
    }
    public void ButtonMouseUp()
    {
        tmpro.transform.DOScale(startScale, scaleDuration);
        tmpro.transform.DOLocalMove(startPosition, moveDuration);
    }
}
