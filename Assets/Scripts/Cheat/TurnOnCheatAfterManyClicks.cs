using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnOnCheatAfterManyClicks : MonoBehaviour
{
    int count = 0;
    float lastClick = 0;

    public void ClickTurnOnCheat()
    {
        Debug.Log($"will show cheat in {20 - count}");
        count++;
        lastClick = Time.time;

        if (count > 20)
        {
            Constants.SHOW_CHEAT_OBJECT = true;
            Constants.SHOW_DEBUG = true;
            var cheats = GameObject.FindObjectsOfType<CheatObject>(true);
            cheats.ToList().ForEach(x => x.gameObject.SetActive(true));
        }
    }

    private void Update()
    {
        if (Time.time - lastClick > 1f)
        {
            count = 0;
        }
    }
}