using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    private Inventory inventory;
    private Transform inventoryPanel;
    private Transform slotTemplate;

    void Awake()
    {
        inventoryPanel = transform.Find("InventoryPanel");
        slotTemplate = inventoryPanel.Find("SlotTemplate");

    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChange += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems() 
    {
        foreach (Transform child in inventoryPanel)
        {
            if (child == slotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        float consumableRowX = 0.0f; 
        float consumableRowY = 0.0f;

        float treaRowX = 0.0f;
        float treaRowY = 1.4f;
        float slotCellSize = 45.0f;

        foreach (Item item in inventory.GetItemList()) 
        {
            RectTransform slotRectTransform = Instantiate(slotTemplate, inventoryPanel).GetComponent<RectTransform>(); 
            
            slotRectTransform.gameObject.SetActive(true);




            slotRectTransform.GetComponent<ButtonUI>().onMouseEnter.AddListener( () => {
                Transform inventoryRoot = slotRectTransform.parent.parent;
                Transform description = inventoryRoot.Find("ItemInfo");

                description.gameObject.SetActive(true);
                TextMeshProUGUI infoDisplay = description.GetComponent<TextMeshProUGUI>();
                switch (item.itemType)
                {
                    case Item.ItemType.Sushi:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Veges:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Meat:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Apple:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Blueberry:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Kiwi:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Strawberry:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Orange:
                        infoDisplay.SetText("");
                        break;
                    case Item.ItemType.Watermelon:
                        infoDisplay.SetText("");
                        break;
                }
            }
            );

            slotRectTransform.GetComponent<ButtonUI>().onMouseExit.AddListener( () => {
                // Transform itemInfo = slotRectTransform.Find("Info");
                Transform inventoryRoot = slotRectTransform.parent.parent;
                Transform description = inventoryRoot.Find("ItemInfo");

                description.gameObject.SetActive(false);

            }
            );

            slotRectTransform.GetComponent<ButtonUI>().onRightClick.AddListener( () => {
                // use the item
                if (item.itemType == Item.ItemType.Sushi || item.itemType == Item.ItemType.Veges || item.itemType == Item.ItemType.Meat)
                {
                    inventory.UseItem(item);
                }
            }
            );







            if (item.itemType == Item.ItemType.Sushi || item.itemType == Item.ItemType.Veges || item.itemType == Item.ItemType.Meat)
            {
                slotRectTransform.anchoredPosition = new Vector2(consumableRowX * slotCellSize, -consumableRowY * slotCellSize);

                Image image = slotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();

                TextMeshProUGUI uiText = slotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
                
                uiText.SetText(item.amount.ToString());

                consumableRowX++;
                if (consumableRowX >= 3) 
                { 
                    consumableRowX = 0;
                    consumableRowY++;
                }
            }
            else
            {
                slotRectTransform.anchoredPosition = new Vector2(treaRowX * slotCellSize, -treaRowY * slotCellSize);

                Image image = slotRectTransform.Find("Image").GetComponent<Image>();
                image.sprite = item.GetSprite();

                TextMeshProUGUI uiText = slotRectTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
                
                uiText.SetText(item.amount.ToString());

                treaRowX++;
                if (treaRowX >= 3) 
                { 
                    treaRowX = 0;
                    treaRowY++;
                }
            }
        }
    }
}