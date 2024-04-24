using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public abstract void UseCard(Hero hero);
    private void OnDisable()
    {
        Destroy(this);
    }
}