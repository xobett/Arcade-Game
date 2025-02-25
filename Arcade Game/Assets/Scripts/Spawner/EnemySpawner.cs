using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class EnemySpawner : MonoBehaviour
{
    [Header("ENEMY SETTINGS")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("SPAWN SETTINGS")]
    [SerializeField] private int limitSpawnDistance;
    [SerializeField] private int timeBetweenSpawns;

    void Start()
    {
        //Starts coroutine that constantly spawns enemies.
        StartCoroutine(Spawner());
    }

    private void SpawnEnemy()
    {
        //Instantiates an enemy in a random Vector2 position, with no rotation at all.
        Instantiate(enemyPrefab, RandomSpawnPosition(), Quaternion.identity);
    }

    private Vector2 RandomSpawnPosition()
    {
        //Creates a Vector2.
        Vector2 spawnPosition;
        //Sets the Vector2 random distance in the X axis.
        spawnPosition.x = Random.Range(-limitSpawnDistance, limitSpawnDistance);
        //Sets the Vector2 Y axis position the same as the spawner's one.
        spawnPosition.y = transform.position.y;

        //Returns the random created Vector2.
        return spawnPosition;
    }

    private IEnumerator Spawner()
    {
        //Waits the amount of seconds set.
        yield return new WaitForSeconds(timeBetweenSpawns);

        //Spawns an enemy in a random position.
        SpawnEnemy();

        //Starts again the coroutine.
        StartCoroutine(Spawner());
    }

    private void OnDrawGizmos()
    {
        Vector2 rightLimitSpawnPosition = transform.right * limitSpawnDistance;
        Vector2 leftLimitSpawnPosition = -transform.right * limitSpawnDistance;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, rightLimitSpawnPosition);
        Gizmos.DrawRay(transform.position, leftLimitSpawnPosition);
    }
}
