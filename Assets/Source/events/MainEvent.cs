public static class MainEvent
{
    // Player events
    public delegate void PlayerHit(AttackDirection direction);
    public static PlayerHit OnPlayerHit;
    public delegate void PlayerDeath();
    public static PlayerDeath OnPlayerDeath;
    public delegate void PlayerTakeDamage();
    public static PlayerTakeDamage OnPlayerTakeDamage;

    // Enemy events
    public delegate void EnemyHit(Enemy _enemy);
    public static EnemyHit OnEnemyHit;
}
