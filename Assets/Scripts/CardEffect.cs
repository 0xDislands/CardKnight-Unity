using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    public abstract void ApplyEffect(Hero hero);
    private void OnDisable()
    {
        Destroy(this);
    }
}