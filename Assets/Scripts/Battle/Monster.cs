using UnityEngine;

public class Monster : CardEffect
{
    public MonsterData monsterData;
    private TextHp textHp;
    [SerializeField] private ParticleSystem attackEffect;

    private void Awake()
    {
        textHp = GetComponentInChildren<TextHp>();
    }
    private void Start()
    {
        Init();
        monsterData.currentHp = monsterData.maxHp;
        textHp.SetHP((int)monsterData.currentHp);
    }

    public override void ApplyEffect(Hero hero)
    {
        var damage = new DamageData();
        damage.damage = monsterData.currentHp;
        hero.TakeDamage(damage, out bool dead);
        if (dead == false)
        {
            hero.AddEXP(monsterData.rewardExp);
            Debug.Log($"add exp {monsterData.rewardExp}");
        }
        var effect = SimpleObjectPool.Instance.GetObjectFromPool(attackEffect, transform.position);
        var card = GetComponent<Card>();
        CardManager.Instance.MoveCardsAfterUse(card);
    }

    public void Init()
    {
        var heroData = CardManager.Instance.hero.heroData;
        monsterData.maxHp = (int)((heroData.level + 1) * monsterData.baseHp * monsterData.multiple);
    }

    public void UpdateHpWhenPlayerLevelUp()
    {
        int hpLost = monsterData.maxHp - monsterData.currentHp;
        Init();
        monsterData.currentHp = monsterData.maxHp - hpLost;
    }

    private void OnDisable()
    {
        Destroy(this);
    }
}