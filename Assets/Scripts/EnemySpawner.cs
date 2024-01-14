using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(Spawner());   
    }


    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            if (enemyPrefabs.Length == 0)
            {
                Debug.LogWarning("enemyPrefabs array is empty.");
                continue; // Skip this iteration and wait for the next
            }
            Debug.Log("Array Length: " + enemyPrefabs.Length);
            int rand = Random.Range(0, enemyPrefabs.Length);
            Debug.Log("Random Index: " + rand);
            GameObject enemyToSpawn = enemyPrefabs[rand];


            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
    }
}
