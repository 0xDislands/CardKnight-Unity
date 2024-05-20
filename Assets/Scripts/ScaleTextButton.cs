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

    private TextMeshProUGUI tmpro;
    private void Awake()
    {
        tmpro = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void ButtonMouseDown()
    {
        tmpro.transform.DOScale(tmpro.transform.localScale + scale, scaleDuration);
        tmpro.transform.DOMove(tmpro.transform.position + movePosition, moveDuration);
    }
    public void ButtonMouseUp()
    {
        tmpro.transform.DOScale(tmpro.transform.localScale - scale, scaleDuration);
        tmpro.transform.DOMove(tmpro.transform.position - movePosition, moveDuration);
    }
}
