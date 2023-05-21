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

    public Sprite gemSprite;
    public Sprite mushroomSprite;
    public Sprite heartSprite;
    public Sprite critSprite;
    public Sprite swiftSprite;
}
