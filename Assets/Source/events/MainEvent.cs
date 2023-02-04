public static class MainEvent
{
    // Player events
    public delegate void OnPlayerHit();
    public delegate void OnPlayerMove();
    public delegate void OnPlayerDeath();

    // Enemy events
    public delegate void OnEnemyHit(Enemy _enemy);

    // Game events
    public delegate void onGameStart();
}
