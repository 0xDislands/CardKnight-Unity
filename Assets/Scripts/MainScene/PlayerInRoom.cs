using TMPro;
using UnityEngine;

public class PlayerInRoom : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtStatus;
    public bool ready { get; private set; }
    private void OnEnable()
    {
        ready = false;
        txtName.text = GetRandomName();
        UpdateDisplay();
    }

    public void SetPlayer()
    {
        txtName.fontStyle = FontStyles.Bold;
        txtName.fontSize += 3f;
        txtStatus.fontStyle = FontStyles.Bold;
        txtStatus.fontSize += 3f;
    }

    public void OnReadyClick()
    {
        ready = !ready;
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
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
