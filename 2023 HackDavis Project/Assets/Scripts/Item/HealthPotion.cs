using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectible
{
    public int healthToRestore;

    public void Collect()
    {
        PlayerStat player = FindObjectOfType<PlayerStat>();
        player.RestoreHealth(healthToRestore);
    }

}
