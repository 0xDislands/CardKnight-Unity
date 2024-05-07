using UnityEngine;
using TMPro;
using System;

public class TextCountDown : MonoBehaviour
{
    public float time;
    public string emptyTime;
    private TextMeshProUGUI txtCountDown;
    private Action onComplete;

    private void Awake()
    {
        txtCountDown = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        txtCountDown.text = TimeSpan.FromSeconds((double)time).ToString("mm\\:ss");
    }

    public void StartCountDown(float time, Action onComplete)
    {
        this.time = time;
        this.onComplete = onComplete;
        gameObject.SetActive(true);
    }
}