using System.Collections;
using UnityEngine;

public class CharlesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject charlesPrefab;
    [SerializeField] private float minSpawnTime = 20f;
    [SerializeField] private float maxSpawnTime = 30f;

    private void Start()
    {
        StartCoroutine(SpawnCharlesRandomly());
    }

    private IEnumerator SpawnCharlesRandomly()
    {
        while (true)
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);

            SpawnCharles();
        }
    }

    private void SpawnCharles()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-5f, 5f), 0);
        Instantiate(charlesPrefab, spawnPosition, Quaternion.identity);

        Debug.Log("Charles spawned!");
    }
}
