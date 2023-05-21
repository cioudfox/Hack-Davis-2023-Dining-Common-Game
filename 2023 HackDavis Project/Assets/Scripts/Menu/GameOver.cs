using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject weapon;
    public GameObject gameOverPanel;

    public void ShowGameOver()
    {
        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);
        GetComponent<PlayerController>().enabled = false;
        weapon.SetActive(false);
        GetComponent<PlayerStat>().enabled = false;
    }
}
