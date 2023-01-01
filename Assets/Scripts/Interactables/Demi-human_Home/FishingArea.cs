using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the area at which Kizuna can fish
public class FishingArea : Conversable
{
    public Item phonenixFish;
    public Item fishingRod;
    public Conversation convo1, convo2, convo3;
    public GameEvent startFishing;
    public GameEvent endFishing;
    private Inventory inventory;
    private static bool isFished;
    private float fishingDuration = 1f;

    void Start() {
        inventory = GameManager.instance.inventory;
    }

    void Update()
    {
        if (isFished)
        this.enabled = false;

        if (!inventory.Contains(fishingRod))
        return;

        TryInteract();
    }

    public override void Interact()
    {
        DialogueManager.instance.StartConversation(convo3);
    }

    public void UseFishingRod()
    {
        if (!playerInRange)
        return;

        StartCoroutine(checkIfHaveFish());
    }

    IEnumerator checkIfHaveFish() 
    {
        startFishing.TriggerEvent();
        
        // checks if phoenix fish is in the inventory,
        // if so, trigger the success conversation,
        // if not trigger the failure conversation.
        // Wait for some time for the fish to be added 
        // when the player triggers the collider of the PhoenixFish
        // to the inventory before checking, since the player
        // may trigger the collider of the PhoenixFish and this object
        // at the same time.

        yield return new WaitForSeconds(0.1f);
       
        if (inventory.Contains(phonenixFish))
        {
            DialogueManager.instance.StartConversation(convo2);
            isFished = true;
        } 
        else
        {
            DialogueManager.instance.StartConversation(convo1);
            Debug.Log("fishing rod used");
        } 

        yield return new WaitForSeconds(fishingDuration);
        endFishing.TriggerEvent();

    }
}