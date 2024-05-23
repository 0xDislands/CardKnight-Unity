using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Action<Monster> onMonsterDead;
    public Action<Card> onNewCardSpawned;
    public Action<Card> onCardDisappeared;

    private void Awake()
    {
        Instance = this;
    }
}