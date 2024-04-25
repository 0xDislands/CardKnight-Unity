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
        textHp.SetHP(monsterData.baseHp);
    }

    public override void ApplyEffect(Hero hero)
    {
        var damage = new DamageData();
        damage.damage = monsterData.baseHp;
        hero.TakeDamage(damage);
    }

    public int GetHP()
    {
        return monsterData.baseHp; 
    }

    private void OnDisable()
    {
        Destroy(this);
    }
}