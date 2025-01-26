using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private int maxNumberOfEnemy;
    [SerializeField] private int spawningCooldown;
    [SerializeField] private int startNumber;
    [SerializeField] private List<GameObject> enemyPrefabList = new();
    [SerializeField] private Vector3 spawnZoneSize;
    [SerializeField] private Vector3 spawnZoneHoleSize;
    [SerializeField] private float playerNoSpawnRadius;
    [SerializeField] private int maxAttemptForSpawningEnemy;

    private bool spawnNextEnemy;
    private float lastSpawnTime;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnZoneSize);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnZoneHoleSize);
        Gizmos.DrawWireSphere(player.position, playerNoSpawnRadius);
    }

    private void Start()
    {
        if (startNumber > maxNumberOfEnemy)
        {
            startNumber = maxNumberOfEnemy;
        }

        for (int i = 0; i < startNumber; i++)
        {
            while (!SpawnEnemy()) { }
        }

        lastSpawnTime = spawningCooldown;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.pause)
        {
            lastSpawnTime -= Time.fixedDeltaTime;

            if (lastSpawnTime <= 0)
            {
                SpawnEnemy();
            }
        }
    }

    private bool SpawnEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Count() < maxNumberOfEnemy)
        {
            Vector3 spawnPos = getValidSpawnPoint();

            if (spawnNextEnemy)
            {
                int enemyIndex = Random.Range(0, enemyPrefabList.Count);
                Instantiate(enemyPrefabList[enemyIndex], transform.position + spawnPos, Quaternion.identity);
                lastSpawnTime = spawningCooldown;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public Vector3 getValidSpawnPoint()
    {
        bool found = false;
        int count = 0;
        Vector3 spawnpoint = new();
        spawnNextEnemy = false;
        while (!found && count < maxAttemptForSpawningEnemy)
        {
            count++;
            spawnpoint = getRandomPosInSpawnZone();
            if (spawnpoint.x >= spawnZoneHoleSize.x/2 || spawnpoint.x <= -spawnZoneHoleSize.x/2 || spawnpoint.z >= spawnZoneHoleSize.z/2 || spawnpoint.z <= -spawnZoneHoleSize.z / 2)
            {
                if ((player.position - spawnpoint).sqrMagnitude >= playerNoSpawnRadius * playerNoSpawnRadius)
                {
                    found = true;
                    spawnNextEnemy = true;
                }
            }
        }
        return spawnpoint;
    }

    private Vector3 getRandomPosInSpawnZone()
    {
        float randX = Random.Range(spawnZoneSize.x / 2, -spawnZoneSize.x / 2);
        float randY = Random.Range(spawnZoneSize.z / 2, -spawnZoneSize.z / 2);

        return new Vector3(randX, 0, randY);
    }
}
