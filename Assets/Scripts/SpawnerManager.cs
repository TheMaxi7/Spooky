using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform[] spawnPoint;
    public float startingCD = 5f;
    public float cdWaveTimer;
    private int waveNumber = 0;
    public int numberOfWaves = 8;
    public int enemyNumber = 1;
    public static List<GameObject> enemiesInScene = new List<GameObject>();

    void Update()
    {
        if (waveNumber <= numberOfWaves)
        {
            cdWaveTimer = Random.Range(10, 20);
            //if dungeon fight started
            if (startingCD <= 0)
            {
                StartCoroutine(SpawnWave());
                
                startingCD = cdWaveTimer;
            }
            startingCD -= Time.deltaTime;
        }

    }
    IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        waveNumber++;
        enemyNumber += 1;
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            GameObject enemyToIstantiate = enemyPrefab[Random.Range(0, enemyPrefab.Length)];
            var enemyToSpawn = Instantiate(enemyToIstantiate, spawnPoint[i].position, enemyToIstantiate.transform.rotation);
            enemiesInScene.Add(enemyToSpawn);
            //agent = enemyInstance.GetComponent<NavMeshAgent>();
            //agent.destination = target.position;
        }

    }

}
