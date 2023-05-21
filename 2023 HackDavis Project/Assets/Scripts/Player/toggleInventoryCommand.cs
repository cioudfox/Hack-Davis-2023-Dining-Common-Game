using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Command
{
    public class toggleInventoryCommand : ScriptableObject, IPlayerCommand
    {
        // Start is called before the first frame update
        GameObject inventoryGameObject;
        void Awake(){
            inventoryGameObject = GameObject.FindGameObjectWithTag("Inventory");
            inventoryGameObject.SetActive(false);
        }
        
        public void Execute(GameObject gameObject)
        {
            if (inventoryGameObject.activeInHierarchy)
            {
                inventoryGameObject.SetActive(false);
            }
            else
            {
                inventoryGameObject.SetActive(true);
            }
        }
    }
}