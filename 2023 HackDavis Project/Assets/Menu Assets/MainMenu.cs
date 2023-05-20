using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI myLabel;
    private int number;
    void Start()
    {
        number = 0;
        Debug.Log("In start");
    }

    public void LoadMenu ()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadAlmanac ()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadTodayMenu ()
    {
        SceneManager.LoadScene(3);
    }
    public void QuitGame ()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}
