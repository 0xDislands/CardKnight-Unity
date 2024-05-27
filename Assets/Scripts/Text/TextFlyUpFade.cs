using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlyUpFade : MonoBehaviour
{
    public float effectTime = 2f;
    public float moveDistance = 1.5f;
    public Color startColor;
    public TextMeshPro text;
    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }
    private void OnEnable()
    {
        StartCoroutine(IEEffect());
    }
    public IEnumerator IEEffect()
    {
        text.color = startColor;
        text.DOFade(0f, effectTime);
        transform.DOMove(transform.position + new Vector3(0f, moveDistance), effectTime).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        yield return null;
    }
}