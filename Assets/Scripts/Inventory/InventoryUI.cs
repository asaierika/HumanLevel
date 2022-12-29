using UnityEngine;
using UnityEngine.EventSystems;

// Must be placed under a singleton parent.
public class InventoryUI : MonoBehaviour
{
    public ZoomInBox zoomBox;
    public ItemObtainedHint itemHint;

    public Transform itemsParent;
    public GameObject panel;

    Inventory inventory;
    InventorySlot[] slots;
    public UiStatus uiStatus;

    public GameObject firstSlot;

    // Start is called before the first frame update
    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventory = GameManager.instance.inventory;
        if (slots.Length != inventory.capacity)
        {
            Debug.LogError("The number of inventory slots should match the capacity of inventory");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !DialogueManager.instance.inDialogue)
        {
            if (!panel.activeInHierarchy)
            {
                Enable();
            }
            else
            {
                Disable();
            }

        }
    }

    // Function only need to be registered when inventory panel is active.
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                Debug.Log(inventory.items.Count);
                slots[i].SetItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void Enable()
    {
        inventory.onItemChangedCallback += UpdateUI;
        inventory.onNewItemAddedCallback += ShowItemHint;
        inventory.onItemZoomedInCallback += ZoomToShowItem;
        // The inventory is closed every time a special item is used.
        inventory.onSpecialItemUsedCallback += Disable;
        UpdateUI();
        panel.SetActive(true);

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
        inventory.onItemChangedCallback -= UpdateUI;
        inventory.onNewItemAddedCallback -= ShowItemHint;
        inventory.onItemZoomedInCallback -= ZoomToShowItem;
        inventory.onSpecialItemUsedCallback -= Disable;
        panel.SetActive(false);
        // Restore the movement of the player
        uiStatus.CloseUI();
    }

    public void ZoomToShowItem(Item item) {
        zoomBox.Show(item);
    }   

    public void ShowItemHint(Item item) {
        itemHint.Show(item);
    }
}
