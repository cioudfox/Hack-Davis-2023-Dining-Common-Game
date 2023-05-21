using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    TextMeshProUGUI text;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateScore()
    {
        Debug.Log("UpdateScore");
        PlayerController pc = FindObjectOfType<PlayerController>();
        Debug.Log("GetComponent");
        int score = pc.GetScore();
        Debug.Log("final"+score );
        text.text = score.ToString("0000");
    }
}