using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dislands;

public class PopupWaiting : MonoBehaviour
{
    public List<PlayerInRoom> players;
    private int playerIndex;
    private bool changingScene;
    public TextCountDown textCountDown;

    private void Awake()
    {
        players = GetComponentsInChildren<PlayerInRoom>().ToList();
    }
    private void OnEnable()
    {
        playerIndex = Random.Range(0, players.Count);
        players[playerIndex].SetPlayer();   
        FakeReady();
        textCountDown.StartCountDown(20, null);
    }

    public void OnReadyClick()
    {
        players[playerIndex].OnReadyClick();
    }

    public bool CheckAllReady()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].ready == false) return false;
        }
        return true;
    }

    void FakeReady()
    {
        var list = new List<PlayerInRoom>();
        list.AddRange(players);
        list.Remove(players[playerIndex]);
        list.Shuffle();
        for (int i = 0; i < list.Count; i++)
        {
            var player = list[i];
            LeanTween.delayedCall(Random.Range(0f, 3f), () =>
            {
                player.OnReadyClick();
            });
        }
    }

    private void Update()
    {
        if (changingScene) return;

        bool allReady = CheckAllReady();
        if (allReady || textCountDown.time <= 0)
        {
            changingScene = true;
            SceneManager.LoadScene("gameplay");
        }
    }
}