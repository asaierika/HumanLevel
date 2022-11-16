using UnityEngine;
using UnityEngine.EventSystems;

// Must be placed under a singleton parent.
public class InventoryUI : MonoBehaviour
{
    public ZoomInBox zoomBox;

    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot[] slots;
    public UiStatus uiStatus;

    public GameObject firstSlot;

    // Start is called before the first frame update
    void Start()
    {
        SetInventory(inventory);
        inventory.onItemChangedCallback += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !DialogueManager.inDialogue)
        {
            if (!inventoryUI.activeInHierarchy)
            {
                Enable();
            }
            else
            {
                Disable();
            }

        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].SetItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void Enable()
    {
        inventoryUI.SetActive(true);

        // Freeze the movement of the player
        uiStatus.OpenUI();

        //Set the first selected object to be the first slot
        if (inventory.Size() != 0)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSlot);
        }
    }

    public void Disable()
    {
        inventoryUI.SetActive(false);
        // Restore the movement of the player
        uiStatus.CloseUI();
    }

    public void ZoomToShowItem(Item item) {
        zoomBox.Show(item);
    }   

    public void SetInventory(Inventory inventory) {
        this.inventory = inventory;
    }
}
