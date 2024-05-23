using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OneTimeClickButton : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        //button = GetComponent<Button>();
        //button?.onClick.AddListener(() =>
        //{
        //    button.interactable = false;
        //});
        //Debug.Log("awake");

    }
    private void OnEnable()
    {
        //Debug.Log("enable");
        //button.interactable = true;
    }

}
