using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatObject : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(Constants.SHOW_CHEAT_OBJECT);
    }
}