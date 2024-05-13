using Dislands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FindMatchManager : MonoBehaviour
{
    public const float FIND_TIME = 4f;
    public static FindMatchManager Instance;
    public Canvas canvasHome;
    public PopupWaiting popupWaiting;
    public TextMeshProUGUI txtButton;
    public TextMeshProFader txtStatus;
    public FindMatchState state;
    private float startFindingTime;

    private void Awake()
    {
        Instance = this;
        txtStatus.gameObject.SetActive(false);
    }

    public void OnSelectHeroClick()
    {

    }

    public void OnFindMatchClick()
    {
        if (state == FindMatchState.Lobby)
        {
            txtStatus.gameObject.SetActive(true);
            txtButton.text = "CANCEL";
            state = FindMatchState.FindingMatch;
            startFindingTime = Time.time;
        } else if (state == FindMatchState.FindingMatch)
        {
            txtStatus.gameObject.SetActive(false);
            txtButton.text = "FIND MATCH";
            state = FindMatchState.Lobby;
        }
    }

    private void Update()
    {
        if (state == FindMatchState.FindingMatch && Time.time - startFindingTime > FIND_TIME)
        {
            state = FindMatchState.EnterRoom;
            EnterRoom();
        }
    }

    public void EnterRoom()
    {
        StartCoroutine(IEEnterRoom());
    }

    public IEnumerator IEEnterRoom()
    {
        txtStatus.textMesh.text = "Found Room";
        yield return new WaitForSeconds(1f);
        canvasHome.gameObject.SetActive(false);
        popupWaiting.gameObject.SetActive(true);
    }
}