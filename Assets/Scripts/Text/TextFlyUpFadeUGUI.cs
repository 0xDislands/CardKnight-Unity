using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class TextFlyUpFadeUGUI : MonoBehaviour
{
    public float effectTime = 2f;
    //public float moveDistance = 1.5f;
    public Color startColor;
    public TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        StartCoroutine(IEEffect());
    }
    public IEnumerator IEEffect()
    {
        text.color = startColor;
        text.DOFade(0f, effectTime);
        text.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        transform.DOMove(transform.position + new Vector3(0f, 200f), effectTime).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
        yield return null;
    }
}