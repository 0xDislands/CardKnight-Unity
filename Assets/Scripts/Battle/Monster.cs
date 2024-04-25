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
        textHp.SetHP(monsterData.hp);
    }

    public override void ApplyEffect(Hero hero)
    {
        var damage = new DamageData();
        damage.damage = monsterData.hp;
        hero.TakeDamage(damage);
    }

    public int GetHP()
    {
        return monsterData.hp; 
    }

    private void OnDisable()
    {
        Destroy(this);
    }
}