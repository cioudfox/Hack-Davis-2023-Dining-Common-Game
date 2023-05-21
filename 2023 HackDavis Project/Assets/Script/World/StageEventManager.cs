using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageScriptableObject stageData;
    [SerializeField] EnemySpawner enemySpawner;

    StageTime stageTime;
    int eventIndex;
    int spawnCount = 0;
    float timer;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Update()
    {
        if (eventIndex >= stageData.stageEvents.Count) { return;}

        timer += Time.deltaTime;
        
        if (stageTime.time > stageData.stageEvents[eventIndex].time)
        {
            if(spawnCount < stageData.stageEvents[eventIndex].count)
            {
                if (timer >= stageData.stageEvents[eventIndex].spawnInterval)
                {
                    timer = 0f;
                    enemySpawner.SpawnEnemy(stageData.stageEvents[eventIndex].enemyToSpawn);
                    spawnCount++;
                }
            }

            if(spawnCount == stageData.stageEvents[eventIndex].count)
            {
                spawnCount = 0;
                eventIndex++;
            }
        }
    }
}
