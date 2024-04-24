public class PoisonEachTurn : TurnEndEffect
{
    public const int DEFAULT_TURN_COUNT = 3;
    public int turnCount = DEFAULT_TURN_COUNT;
    public int damage;
    private Hero hero;
    private DamageData damageData;
    private void Awake()
    {
        hero = GetComponent<Hero>();
        damageData = new DamageData();
        damageData.damage = damage;
    }
    private void OnEnable()
    {
        turnCount = DEFAULT_TURN_COUNT;
    }
    public override void OnTurnEnd()
    {
        turnCount--;
        if (turnCount < 0) Destroy(this);
        hero.TakeDamage(damageData);
    }
}