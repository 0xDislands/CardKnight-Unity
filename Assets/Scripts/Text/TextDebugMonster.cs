using UnityEngine;
using TMPro;

public class TextDebugMonster : MonoBehaviour
{
    private TextMeshProUGUI txtDebug;
    private Monster monster;

    private void Awake ()
    {
        monster = GetComponentInParent<Monster> ();
        txtDebug = GetComponent<TextMeshProUGUI> ();
        txtDebug.text = "";
    }

    private void Update ()
    {
        if (Constants.SHOW_DEBUG == false) return;
        txtDebug.text = GetDebugText ();
    }

    public string GetDebugText ()
    {
        var data = monster.monsterData;
        string str = "";
        str += $"exp: {data.rewardExp} \n";
        str += $"base: {data.baseHp} \n";
        str += $"multiple: {data.multiple} \n";
        return str;
    }
}