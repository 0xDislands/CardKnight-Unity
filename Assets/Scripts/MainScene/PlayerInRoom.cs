using TMPro;
using UnityEngine;

public class PlayerInRoom : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtStatus;
    public bool ready { get; private set; }
    public bool isPlayer { get; private set; }
    private int bigFontSize = 60;
    private int normalFontSize = 54;

    private void Awake()
    {
        txtName.text = GetRandomName();
    }
    public void SetPlayer(bool isPlayer)
    {
        this.isPlayer = isPlayer;
    }
    public void SetReady(bool ready)
    {
        this.ready = ready;
    }
    public void OnReadyClick()
    {
        ready = !ready;
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        txtName.fontStyle = isPlayer ? FontStyles.Bold : FontStyles.Normal;
        txtStatus.fontStyle = isPlayer ? FontStyles.Bold : FontStyles.Normal;
        txtName.fontSize = isPlayer ? bigFontSize : normalFontSize;
        txtStatus.fontSize = isPlayer ? bigFontSize : normalFontSize;
        txtStatus.text = ready ? "Ready" : "Not Ready";
    }
    string alphabet = "abcdefghijklmnopqstuvxyz0123456789";
    public string GetRandomName()
    {
        string str = "0x";
        for (int i = 0; i < 10; i++)
        {
            str += alphabet[Random.Range(0, alphabet.Length)];
        }
        return str;
    }
}
