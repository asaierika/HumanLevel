using UnityEngine;

public class PhoenixFish_Interactable : Interactable
{    
    private Inventory inventory;
    public Item phoenixFishItem;
    public Item fishingRod;

    void Start() 
    {
        inventory = GameManager.instance.inventory;
    }

    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        if (!inventory.Contains(fishingRod))
        return;

        inventory.Add(phoenixFishItem);
        gameObject.SetActive(false);
    }
}
