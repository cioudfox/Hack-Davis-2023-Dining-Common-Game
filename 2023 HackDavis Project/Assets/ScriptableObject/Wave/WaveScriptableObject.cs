using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

[CreateAssetMenu(fileName = "WaveScriptableObject", menuName = "ScriptableObjects/Wave")]
public class WaveScriptableObject : ScriptableObject 
{
    public Wave wave;
}
