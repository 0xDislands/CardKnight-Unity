using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public ParticleSystem effectHit;
    public ParticleSystem effectHeal;
    private void Awake()
    {
        Instance = this;
    }
    public void Hit(Vector2 position)
    {
        var newAttackEffect = SimpleObjectPool.Instance.GetObjectFromPool(effectHit, position);
    }
    public void Heal(Vector2 position)
    {
        var newAttackEffect = SimpleObjectPool.Instance.GetObjectFromPool(effectHeal, position);
    }
}