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
        // Debug.Log("Inventory reference obtained " + (inventory == GameManager.instance.inventory) + " at " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        inventory.onNewItemAddedCallback += ShowItemHint;
        inventory.onItemUsedCallback += ZoomToShowItem;
    }

    void OnEnable() {
        // Debug.Log("Inventory UI enabled at " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        if (inventory != null) {
            // Debug.Log("Inventory UI enabled and setting callbacks");
            inventory.onNewItemAddedCallback += ShowItemHint;
            inventory.onItemUsedCallback += ZoomToShowItem;
        }
    }

    void OnDisable() {
        if (panel.activeInHierarchy) {
            // Ensure UpdateUI is unregistered when panel is inactive.
            Disable();
        }

        // Debug.Log("Decommission inventory UI at " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        // BUG: The check null is only required for the corridor scene. Why is OnDisable being called without Start(). 
        if (inventory != null) {
            inventory.onNewItemAddedCallback -= ShowItemHint;
            inventory.onItemUsedCallback -= ZoomToShowItem;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !DialogueManager.inDialogue)
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
