using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    //public TextMeshProUGUI myLabel;
    //private int number;
    //public Animator transition;

    
    void Start()
    {
        //number = 0;
        Debug.Log("In start");
    }

    public void LoadMenu ()
    {
        //StartCoroutine(LoadLevel(0));
        SceneManager.LoadScene(0);
    }

    public void PlayGame ()
    {
        //StartCoroutine(LoadLevel(1));
        SceneManager.LoadScene(1);
    }

    public void LoadAlmanac ()
    {
        //StartCoroutine(LoadLevel(2));
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
/*
    IEnumerator LoadLevel(int lvlindex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(lvlindex);
    }
*/
}
