using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        
        if (collider2D.gameObject.CompareTag("Occludable"))
        {
            Debug.Log("Occlusion detected");
            SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = "Foreground";
            spriteRenderer.sortingOrder = 3;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Occludable"))
        {
            SpriteRenderer spriteRenderer = collider2D.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }
}
