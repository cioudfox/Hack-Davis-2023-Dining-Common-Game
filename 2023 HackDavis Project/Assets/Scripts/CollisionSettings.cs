using UnityEngine;

public class CollisionSettings : MonoBehaviour
{
    // Define layers for player and enemy
    int playerLayer;
    int enemyLayer;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        // Modify collision matrix to allow only enemy-player collisions
        Physics.IgnoreLayerCollision(playerLayer, enemyLayer, false);

        // Disable collisions between the enemy layer and all other layers
        for (int i = 0; i < 32; i++) // 32 is the maximum number of layers in Unity
        {
            if (i != enemyLayer)
            {
                Physics.IgnoreLayerCollision(enemyLayer, i, true);
            }
        }

        // Assign layer to player object
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        playerObject.layer = playerLayer;

        // Assign layer to enemy object
        gameObject.layer = enemyLayer;
    }
}
