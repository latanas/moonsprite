using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // Attribution: Maceij Wolski + Dominic Rooney

    public GameObject itemActionContainerSceneObject;

    public List<InventoryItem> itemList = new List<InventoryItem>();

    //private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();
    public static event Action<List<InventoryItem>> OnInventoryChange;

    public static Inventory instance = null;

    bool firstOnEnable = true;

    bool newItemFlipFlop = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenericCollectibleItem.OnItemCollected += Add;
    }

    private void OnEnable()
    {
        if (firstOnEnable == false)
        {
            GenericCollectibleItem.OnItemCollected += Add;

            ToolBarUIScript.Instance.UpdateSlots();
        }
        firstOnEnable = true;
    }

    private void OnDisable()
    {
        GenericCollectibleItem.OnItemCollected -= Add;

        ToolBarUIScript.Instance.UpdateSlots();
    }

    public void Add(ItemData itemData)
    {
        InventoryItem newItem = new InventoryItem(itemData);

        newItemFlipFlop = !newItemFlipFlop;
        if (newItemFlipFlop == false)
        {
            itemList.Add(newItem);
        }
        else
        {
            itemList.Insert(0, newItem);
            ToolBarUIScript.Instance.ShiftSelectedSlot(1);
        }

        //itemDictionary.Add(itemData, newItem);

        ToolBarUIScript.Instance.UpdateSlots(); 
    }

    public void Remove(InventoryItem inventoryItem)
    {
        if(itemList.Count == 0)
        {
            return;
        }

        if(itemList.Contains(inventoryItem) == true)
        {
            itemList.Remove(inventoryItem);
        }

        /*
        if (contains == true /*&& itemDictionary.TryGetValue(inventoryItem.itemData, out InventoryItem item))
        {
            itemList.Remove(item);
            itemDictionary.Remove(inventoryItem.itemData);
        }
        */

        ToolBarUIScript.Instance.UpdateSlots();
    }

    public bool LoadItemsActions(InventoryItem inventoryItem)
    {
        if(inventoryItem.itemActionsFailLoaded == true)
        {
            Debug.Log("Load aborted because item action has fail loaded before");
            return false;
        }

        if (inventoryItem.itemActionsLoaded == true)
        {
            Debug.Log("Item's actions are already loaded");
            return true;
        }

        if (inventoryItem.itemData.itemActionPrefab ?? null)
        {
        }
        else
        {
            inventoryItem.itemActionsFailLoaded = true;
            Debug.Log("No itemAction Prefab");
            return false;
        }

        GameObject instantiatedItemActionObject = Instantiate(inventoryItem.itemData.itemActionPrefab, itemActionContainerSceneObject.transform.position, itemActionContainerSceneObject.transform.rotation, itemActionContainerSceneObject.transform);
        inventoryItem.itemActions = instantiatedItemActionObject.GetComponents<IItemAction>();
        inventoryItem.itemActionsLoaded = true;

        if (inventoryItem.itemActions == null)
        {
            inventoryItem.itemActionsFailLoaded = true;
            Debug.Log("itemActions list is null, no interfaced components to get from GameObject");
            return false;
        }

        if (inventoryItem.itemActions.Length == 0)
        {
            inventoryItem.itemActionsFailLoaded = true;
            Debug.Log("itemActions list's length is 0, no interfaced components to get from GameObject");
            return false;
        }

        return true;
    }
}
