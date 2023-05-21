using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance {get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    public Sprite sushiSprite;
    public Sprite vegesSprite;
    public Sprite meatSprite;
    public Sprite appleSprite;
    public Sprite blueberrySprite;
    public Sprite kiwiSprite;

    public Sprite orangeSprite;

    public Sprite strawberrySprite;

    public Sprite watermelonSprite;

}
