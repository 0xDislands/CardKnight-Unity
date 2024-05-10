using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextPowerupName : MonoBehaviour
{
    public PowerupId id;
    private TextMeshProUGUI txt;
    private bool init;

    private void Awake()
    {
        txt = GetComponent<TextMeshProUGUI>();
        txt.text = "";
    }

    private void Update()
    {
        if (CardManager.Instance.hero != null && init == false)
        {
            init = true;
            UpdateDisplay();
        }
    }

    public void UpdateDisplay()
    {
        if (txt == null) txt = GetComponent<TextMeshProUGUI>();
        var data = DataManager.Instance.dicPowerUp[id];
        if (CardManager.Instance.hero.heroData.level < data.unlockLevel)
        {
            txt.text = $"Unlock at level {data.unlockLevel}";
        }
        else
        {
            txt.text = data.name;
        }
    }
}