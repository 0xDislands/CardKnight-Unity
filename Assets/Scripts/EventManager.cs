using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public Action<Monster> onMonsterDead;
    public Action<Card> onNewCardSpawned;
    public Action<Card> onCardDisappeared;
    public UnityEvent OnHeroMove;

    private void Awake()
    {
        Instance = this;
    }
}