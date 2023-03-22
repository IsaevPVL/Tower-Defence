using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;

    [Space, SerializeField] Crystal crystal;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] EnemySpawner enemySpawner;

    void Awake()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        gameOverPanel.SetActive(false);
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        crystal.StopAttacking();
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);

        playerStats.ResetStats();

        enemySpawner.ResetSpawner();

        crystal.DrawAttackRange();
        crystal.StartAttacking();

    }

    void OnEnable()
    {
        Enemy.OnReachingCastle += GameOver;
    }

    void OnDisable()
    {
        Enemy.OnReachingCastle -= GameOver;
    }
}