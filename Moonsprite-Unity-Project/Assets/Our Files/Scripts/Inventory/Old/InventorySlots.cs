using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

[System.Obsolete("Replaced by ToolbarSlotScript")]
public class InventorySlots : MonoBehaviour
{
    /*
    public Image icon;
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI stackSizeText;

    public void ClearSlot()
    {
        icon.enabled = false;
        labelText.enabled = false;
        stackSizeText.enabled = false;
        labelText.fontSize = 10.0f;
        stackSizeText.fontSize = 10.0f;
        labelText.color = Color.green;
        stackSizeText.color = Color.green;
    }

    public void DrawSlot(InventoryItem item)
    {

        if (item == null)
        {
            ClearSlot();
            return;
        }

        icon.enabled = true;
        labelText.enabled = true;
        stackSizeText.enabled = true;

        icon.sprite = item.itemData.icon;
        labelText.text = item.itemData.name;
        stackSizeText.text = item.stacksize.ToString();
        labelText.fontSize = 10.0f;
        stackSizeText.fontSize = 10.0f;
        labelText.color = Color.green;
        stackSizeText.color = Color.green;
    }
    */
}
