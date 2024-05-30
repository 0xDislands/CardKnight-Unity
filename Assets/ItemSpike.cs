using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpike : Item
{
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Image icon;
    [SerializeField] private ParticleSystem attackEffect;
    private bool active;

    private void OnEnable()
    {
        active = !active;
        icon.sprite = onSprite;
    }

    private void Start()
    {
        EventManager.Instance.OnHeroMove.AddListener(ChangeState);
    }

    private void ChangeState()
    {
        if (!gameObject.activeInHierarchy) return;
        active = !active;
        if(active) icon.sprite = onSprite;
        else icon.sprite = offSprite;
    }

    public override void ApplyEffect(Hero hero)
    {
        if (active)
        {
            hero.TakeDamage(new DamageData(hero.heroData.maxHp * 1.3f), out var dead);
            SimpleObjectPool.Instance.GetObjectFromPool(attackEffect, transform.position);
        }

        var card = GetComponent<Card>();
        card.Disappear();
        CardManager.Instance.MoveCardsAfterUse(card);
    }
}
