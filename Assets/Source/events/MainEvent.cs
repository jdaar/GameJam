public static class MainEvent
{
    // Player events
    public delegate void PlayerHit();
    public static PlayerHit OnPlayerHit;
    public delegate void PlayerDeath();
    public static PlayerDeath OnPlayerDeath;

    // Enemy events
    public delegate void EnemyHit(Enemy _enemy);
    public static EnemyHit OnEnemyHit;
}
