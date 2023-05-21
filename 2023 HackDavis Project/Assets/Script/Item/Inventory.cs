using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChange;
    private List<Item> itemList;
    private Action<Item> useItemAction;

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void AddItem(Item item)
    {
        bool itemAlreadyInInventory = false;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount += 1;
                itemAlreadyInInventory = true;
            }
        }
        if (!itemAlreadyInInventory)
        {
            itemList.Add(item);
        }

        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    internal void RemoveItem(Item item)
    {
        Item targetItem = null;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                inventoryItem.amount -= item.amount;
                targetItem = inventoryItem;
            }
        }
        if (targetItem != null && targetItem.amount <= 0)
        {
            itemList.Remove(targetItem);
        }

        OnItemListChange?.Invoke(this, EventArgs.Empty);
    }
}
