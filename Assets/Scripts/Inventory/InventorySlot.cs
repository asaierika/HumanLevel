using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    public Image icon;
    public Text nameOfItem;
    public Text amount;
    public Button InventorySlotButton;
    public static InventoryUI inventoryUI;

    void Start() {
        // Assumes per scene there will only be one inventoryUI.
        if (inventoryUI == null) {
            inventoryUI = GameObject.FindObjectOfType<InventoryUI>();
        }
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        InventorySlotButton.onClick.Invoke();
        */
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
        if (item != null)
            inventoryUI.ZoomToShowItem(item);
    }
}
