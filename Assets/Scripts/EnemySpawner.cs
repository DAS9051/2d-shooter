using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Serialized fields allow these variables to be modified in the Unity Inspector
    // spawnRate determines the time interval between each spawn
    [SerializeField] private float spawnRate = 1f;

    // Array of enemy prefabs to be spawned
    [SerializeField] private GameObject[] enemyPrefabs;

    // Flag to control whether enemies can be spawned or not
    [SerializeField] private bool canSpawn = true;

    // Start is called before the first frame update
    private void Start()
    {
        // Start the Spawner coroutine
        StartCoroutine(Spawner());   
    }

    // Coroutine for spawning enemies
    private IEnumerator Spawner()
    {
        // WaitForSeconds is used to delay the next spawn according to spawnRate
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        // Loop as long as canSpawn is true
        while (canSpawn)
        {
            // Wait for the specified spawnRate time
            yield return wait;

            // Check if the enemyPrefabs array is empty
            if (enemyPrefabs.Length == 0)
            {
                // Log a warning and skip the current iteration
                Debug.LogWarning("enemyPrefabs array is empty.");
                continue;
            }

            // Log the length of the enemyPrefabs array
            Debug.Log("Array Length: " + enemyPrefabs.Length);

            // Generate a random index to select an enemy prefab
            int rand = Random.Range(0, enemyPrefabs.Length);
            Debug.Log("Random Index: " + rand);

            // Select the enemy prefab based on the random index
            GameObject enemyToSpawn = enemyPrefabs[rand];

            // Instantiate (create) the enemy at the position and rotation of the spawner
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
