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
    public void Hit(Transform effectParent)
    {
        var newAttackEffect = SimpleObjectPool.Instance.GetObjectFromPool(effectHit, effectParent.transform.position);
        newAttackEffect.transform.SetParent(effectParent);
    }
    public void Heal(Transform effectParent)
    {
        var newAttackEffect = SimpleObjectPool.Instance.GetObjectFromPool(effectHeal, effectParent.transform.position);
        newAttackEffect.transform.SetParent(effectParent);
    }
}