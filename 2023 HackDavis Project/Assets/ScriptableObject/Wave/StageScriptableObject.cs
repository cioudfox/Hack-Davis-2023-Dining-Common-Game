using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageEvent
{
    public float time;
    public List<GameObject> enemyToSpawn;
    public int count;

    public float spawnInterval;
}

[CreateAssetMenu(fileName = "StageScriptableObject", menuName = "ScriptableObjects/Stage")]
public class StageScriptableObject : ScriptableObject 
{
    public List<StageEvent> stageEvents;    
}
