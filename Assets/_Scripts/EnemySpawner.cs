using System.Collections;
using UnityEngine;
using TMPro;

public sealed class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnRadius = 10f;
    [SerializeField] float delayBetweenSpawns;
    WaitForSeconds delay;
    WaitForSeconds dramaticPause;
    Vector3 currentEnemySpawnPosition, towerLocation = new Vector3(0f, 0.35f, 0f);
    int enemiesToSpawnNext;
    int enemiesLeft;

    [Header("Counter"), SerializeField] TMP_Text enemyCounter;

    void Start()
    {
        delay = new WaitForSeconds(delayBetweenSpawns);
        dramaticPause = new WaitForSeconds(1f);

        StartCoroutine(StartNewWave());
    }

    IEnumerator StartNewWave()
    {
        yield return dramaticPause;

        enemiesToSpawnNext = Random.Range(3, 8);
        enemyCounter.text = enemiesToSpawnNext.ToString();

        enemiesLeft = enemiesToSpawnNext;

        for (int i = 0; i < enemiesToSpawnNext; i++)
        {
            currentEnemySpawnPosition = transform.position;
            Vector2 temp = UnityEngine.Random.insideUnitCircle.normalized * spawnRadius;
            currentEnemySpawnPosition.x = temp.x;
            currentEnemySpawnPosition.z = temp.y;

            Instantiate(enemy, currentEnemySpawnPosition, Quaternion.LookRotation(transform.position - currentEnemySpawnPosition)).transform.SetParent(transform);

            yield return delay;
        }
    }

    void EnemyDied()
    {
        enemiesLeft--;

        enemyCounter.text = enemiesLeft.ToString();

        if (enemiesLeft == 0)
        {
            StartCoroutine(StartNewWave());
        }
    }

    public void ResetSpawner()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        enemyCounter.text = "0";

        StartCoroutine(StartNewWave());
    }

    void OnEnable()
    {
        Enemy.OnEnemyDying += EnemyDied;
        Enemy.OnReachingCastle += StopAllCoroutines;
    }

    void OnDisable()
    {
        Enemy.OnEnemyDying -= EnemyDied;
        Enemy.OnReachingCastle -= StopAllCoroutines;
    }
}