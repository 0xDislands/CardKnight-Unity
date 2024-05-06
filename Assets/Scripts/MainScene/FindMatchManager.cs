using System.Collections;
using TMPro;
using UnityEngine;

public class FindMatchManager : MonoBehaviour
{
    public const float FIND_TIME = 4f;

    public TextMeshProUGUI txtButton;
    public TextMeshProFader txtStatus;
    public FindMatchState state;
    private float startFindingTime;

    private void Awake()
    {
        txtStatus.gameObject.SetActive(false);
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
            StartCoroutine(IEEnterRoom());
        }
    }

    public IEnumerator IEEnterRoom()
    {
        txtStatus.textMesh.text = "Found Room";
        yield return new WaitForSeconds(1f);

        txtStatus.textMesh.text = "Entering Room";
        yield return new WaitForSeconds(2f);

        Debug.Log("enter room!");
    }
}