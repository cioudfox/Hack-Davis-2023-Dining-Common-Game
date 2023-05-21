using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanelManager : MonoBehaviour
{
   [SerializeField] GameObject panel;
   PauseManager pauseManager;

   private void Awake() 
   {
        pauseManager = GetComponent<PauseManager>();
   }

   void Update()
   {

   }
    public void CloseMenu()
    {
        pauseManager.UnPauseGame();
        panel.SetActive(false);
    }
    public void OpenMenu()
    {
        pauseManager.PauseGame();
        panel.SetActive(true);
    }
}
