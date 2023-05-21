using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTime : MonoBehaviour
{
    public float time;
    TimerUI timerUi;

    private void Awake() 
    {
        timerUi = FindObjectOfType<TimerUI>();
    }
    private void Update() 
    {
        time += Time.deltaTime;
        timerUi.UpdateTime(time);
    }

}
