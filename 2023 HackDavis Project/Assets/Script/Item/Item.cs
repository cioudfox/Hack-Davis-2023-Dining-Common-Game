using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType 
    {
        Sushi,
        Veges,
        Meat,
        Apple,
        Blueberry,
        Kiwi,
        Orange,
        Strawberry,
        Watermelon
    }

    public ItemType itemType;
    public int amount;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            case ItemType.Sushi:
                return ItemAssets.Instance.sushiSprite;
            case ItemType.Veges:
                return ItemAssets.Instance.vegesSprite;
            case ItemType.Meat:
                return ItemAssets.Instance.meatSprite;
            case ItemType.Apple:
                return ItemAssets.Instance.appleSprite;
            case ItemType.Blueberry:
                return ItemAssets.Instance.blueberrySprite;
            case ItemType.Kiwi:
                return ItemAssets.Instance.kiwiSprite;
            case ItemType.Orange:
                return ItemAssets.Instance.orangeSprite;
            case ItemType.Strawberry:
                return ItemAssets.Instance.strawberrySprite;
            case ItemType.Watermelon:
                return ItemAssets.Instance.watermelonSprite;
            default:
                Debug.Log("Invalid Item Type.");
                return null;
        }
    }
}
