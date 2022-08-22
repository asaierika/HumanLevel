using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot[] slots;

    public GameObject firstSlot;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
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
                slots[i].AddItem(inventory.items[i]);
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
        GameEvents.instance.OpenUI();

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
        GameEvents.instance.CloseUI();
    }
}
