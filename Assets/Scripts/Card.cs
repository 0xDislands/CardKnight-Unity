using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private Image imgIcon;
    [SerializeField] Sprite[] sprites;

    private void Start()
    {
        imgIcon.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}

public class GameFlow : MonoBehaviour
{

}

public class Character : MonoBehaviour
{

}

public class CardManager : MonoBehaviour
{

}