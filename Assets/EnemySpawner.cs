using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject NinjaPrefab;

    [SerializeField]
    private float spawnInterval = 5f;

  
    [SerializeField]
    private int maxEnemies = 5;

    void Start()
    {
        
        StartCoroutine(SpawnEnemyLoop());
    }

    private IEnumerator SpawnEnemyLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Count existing enemies by tag. "Enemy".
            int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            // Spawn only if current enemies are less than the allowed maximum
            if (enemyCount < maxEnemies && NinjaPrefab != null)
            {
                Vector3 spawnPos = transform.position;
                Instantiate(NinjaPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
