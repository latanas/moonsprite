using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header("Required References")]
    public ToolBarUIScript toolBarUIScript;

    [Header("Item List")]
    public List<InventoryItem> inventory = new List<InventoryItem>();

    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();
    public static event Action<List<InventoryItem>> OnInventoryChange;

    private void OnEnable()
    {
        GenericCollectibleItem.OnItemCollected += Add;

        toolBarUIScript.UpdateInventoryVisualDisplay();
    }

    private void OnDisable()
    {
        GenericCollectibleItem.OnItemCollected -= Add;

        toolBarUIScript.UpdateInventoryVisualDisplay();
    }

    public void Add(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            OnInventoryChange?.Invoke(inventory);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            OnInventoryChange?.Invoke(inventory);
        }

        toolBarUIScript.UpdateInventoryVisualDisplay();
    }


    public void Remove(ItemData itemData)
    {
        if (itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {

            item.RemoveFromStack();
            if(item.stacksize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
            OnInventoryChange?.Invoke(inventory);
        }

        toolBarUIScript.UpdateInventoryVisualDisplay();
    }

    public List<InventoryItem> GetListOfItems()
    {

        return inventory;
    }

}
