using UnityEngine;

public class Monster : CardEffect
{
    public MonsterData monsterData;
    private TextHp textHp;

    private void Awake()
    {
        textHp = GetComponentInChildren<TextHp>();
    }
    private void Start()
    {
        UpdateMaxHp();
        monsterData.currentHp = monsterData.maxHp;
        textHp.SetHP((int)monsterData.currentHp);
    }

    public override void ApplyEffect(Hero hero)
    {
        var damage = new DamageData();
        damage.damage = monsterData.currentHp;
        hero.TakeDamage(damage);
    }

    public void UpdateMaxHp()
    {
        var heroData = CardManager.Instance.hero.heroData;
        monsterData.maxHp = (int)((heroData.level + 1) * monsterData.baseHp * monsterData.multiple);
    }

    public void UpdateHpWhenPlayerLevelUp()
    {
        int hpLost = monsterData.maxHp - monsterData.currentHp;
        UpdateMaxHp();
        monsterData.currentHp = monsterData.maxHp - hpLost;
    }

    private void OnDisable()
    {
        Destroy(this);
    }
}