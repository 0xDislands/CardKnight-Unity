using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance;

    public Queue<System.Action> actions = new Queue<System.Action>();
    private void Awake()
    {
        Instance = this;
    }
    public void ShowInQueue(System.Action action)
    {
        actions.Enqueue(action);
        if (actions.Count == 1)
        {
            DoNextAction();
        }
    }

    public void DoNextAction()
    {
        if (actions.Count == 0) return;
        var action = actions.Dequeue();
        action?.Invoke();
    }
}