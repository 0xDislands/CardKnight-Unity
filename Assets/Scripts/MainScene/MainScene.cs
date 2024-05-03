using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScene : MonoBehaviour
{
    public const string SCENE_GAMEPLAY = "Gameplay";
    public Canvas canvasStart;

    private void Start()
    {
        canvasStart.gameObject.SetActive(true);
    }

    public void Play()
    {
        var canvasGroup = canvasStart.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, 1f).OnComplete(() =>
        {
            canvasStart.gameObject.SetActive(false);
        });
        CardManager.Instance.StartGame();
    }
}