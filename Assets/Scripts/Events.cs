using MoreMountains.Tools;

public struct EnemyDestroyed
{
    public Enemy Enemy;
    public EnemyDestroyed(Enemy enemy)
    {
        Enemy = enemy;
    }
    static EnemyDestroyed e;
    public static void Trigger(Enemy newEnemy)
    {
        e.Enemy = newEnemy;
        MMEventManager.TriggerEvent(e);
    }
}

public struct EnemyReachedEndPositon
{
    public Enemy Enemy;
    public EnemyReachedEndPositon(Enemy enemy)
    {
        Enemy = enemy;
    }
    static EnemyReachedEndPositon e;
    public static void Trigger(Enemy newEnemy)
    {
        e.Enemy = newEnemy;
        MMEventManager.TriggerEvent(e);
    }
}

public struct CoinCountChanged
{
    public int ChangeAmount;
    public CoinCountChanged(int changeAmount)
    {
        ChangeAmount =  changeAmount;
    }
    static CoinCountChanged e;
    public static void Trigger(int newChangeAmount)
    {
        e.ChangeAmount = newChangeAmount;
        MMEventManager.TriggerEvent(e);
    }
}

public struct PlayerHpChanged
{
    public int ChangeAmount;
    public PlayerHpChanged(int changeAmount)
    {
        ChangeAmount =  changeAmount;
    }
    static PlayerHpChanged e;
    public static void Trigger(int newChangeAmount)
    {
        e.ChangeAmount = newChangeAmount;
        MMEventManager.TriggerEvent(e);
    }
}

public struct GameEnded
{
    static GameEnded e;
    public static void Trigger()
    {
        MMEventManager.TriggerEvent(e);
    }
}
