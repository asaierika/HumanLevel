using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    public Image icon;
    public Text nameOfItem;
    public Text amount;
    public Button InventorySlotButton;
    public static Inventory inventory;

    void Start() {
        // Assumes per scene there will only be one inventoryUI.
        if (inventory == null) {
            inventory = GameManager.instance.inventory;
        }
    }

    public void SetItem(Item newItem)
    {
        item = newItem;
       
        icon.sprite = item.icon;
        nameOfItem.text = item.nameOfItem;
        amount.text = item.amount.ToString();

        icon.enabled = true;
        nameOfItem.enabled = true;
        amount.enabled = true;
        InventorySlotButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        nameOfItem.text = "";
        amount.text = "";

        icon.enabled = false;
        nameOfItem.enabled = false;
        amount.enabled = false;
        InventorySlotButton.interactable = false;
    }

    public void UseItem()
    {
        if (item != null) {
            inventory.UseItem(item);
        }
    }
}
