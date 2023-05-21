using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType 
    {
        Gem,
        Mushroom,
        Heart,
        CriticalSurge,
        Swift
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Gem:
                return ItemAssets.Instance.gemSprite;
            case ItemType.Mushroom:
                return ItemAssets.Instance.mushroomSprite;
            case ItemType.Heart:
                return ItemAssets.Instance.heartSprite;
            case ItemType.CriticalSurge:
                return ItemAssets.Instance.critSprite;
            case ItemType.Swift:
                return ItemAssets.Instance.swiftSprite;
            default:
                Debug.Log("Invalid Item Type.");
                return null;
        }
    }
}
