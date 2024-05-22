using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TagDisplay : MonoBehaviour
{
    public CanvasGroup group;
    [SerializeField] private TextMeshProUGUI infoTxt;
    [SerializeField] private Image img;
    public void Display(Sprite icon, string info)
    {
        img.sprite = icon;
        infoTxt.text = info;
    }
}
