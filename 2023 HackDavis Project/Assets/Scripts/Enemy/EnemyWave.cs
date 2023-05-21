using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;

    // [SerializeField] List<WaveScriptableObject> waveData;

    // [SerializeField] EnemySpawner enemySpawner;

    [Serializable]
    public class Wave
    {
        public List<EnemyGroup> enemyGroups;
        public int waveQuota; // The total number of enemies spawned in this wave
        public float spawnInterval; //Interval between spawn times
        public int totalSpawnCount; // The number of enemies already spawned
    }

    [Serializable]
    public class EnemyGroup
    {
        public GameObject enemyPrefab;
        public int enemyCount; // The number of enemy in this wave
        public int spawnCount;
    }

    public List<Wave> waveData;

    public int currentWaveIndex = 0;

    [Header("Spawner Attributes")]
    float timer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool isMaxEnemies;
    public float waveInterval;
    void Start()
    {
        CalculateQuata(); 
    }

    void Update()
    {
        if(currentWaveIndex < waveData.Count && waveData[currentWaveIndex].totalSpawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
        timer += Time.deltaTime;
        if (timer >= waveData[currentWaveIndex].spawnInterval)
        {
            timer = 0f;
            spawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if (currentWaveIndex < waveData.Count -1)
        {
            currentWaveIndex++;
            CalculateQuata();
        }
    }

    void CalculateQuata()
    {
        int currentWaveQuata = 0;
        foreach(var enemyGroup in waveData[currentWaveIndex].enemyGroups)
        {
            currentWaveQuata += enemyGroup.enemyCount;
        }

        waveData[currentWaveIndex].waveQuota = currentWaveQuata;
        Debug.LogWarning(currentWaveQuata);
    }

    /*
    Troblue with the spawning and calculation. Need to find the way to reset 
    parameters in the scriptable object.
    */

    void spawnEnemies()
    {
        if (waveData[currentWaveIndex].totalSpawnCount < waveData[currentWaveIndex].waveQuota&& !isMaxEnemies)
        {            
            foreach(var enemyGroup in waveData[currentWaveIndex].enemyGroups)
            {
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        isMaxEnemies = true;
                        return;
                    }
                    Vector3 position = GenerateRandomPosition();

                    position += player.transform.position;

                    GameObject newEnemy = Instantiate(enemyGroup.enemyPrefab, position, Quaternion.identity);
                    newEnemy.GetComponent<EnemyController>().SetTarget(player);
                    newEnemy.transform.parent = transform;

                    enemyGroup.spawnCount++;
                    waveData[currentWaveIndex].totalSpawnCount++;
                    enemiesAlive++;
                }
            }
        }

        if(enemiesAlive < maxEnemiesAllowed)
        {
            isMaxEnemies = false;
        }
    }
    public Vector3 GenerateRandomPosition()
    {
        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        Vector3 position = new Vector3();

        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x,  spawnArea.x);
            position.y = spawnArea.y * f;   
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y,  spawnArea.y);
            position.x = spawnArea.x * f;   
        }
        // position.x = UnityEngine.Random.Range(-spawnArea.x,  spawnArea.x);
        // position.y = UnityEngine.Random.Range(-spawnArea.y,  spawnArea.y);
        position.z = 0.0f;
        return position;        
    }

    public void OnEnemyKilled()
    {
        Debug.Log("Enemy killed called!");
        enemiesAlive--;
    }
}