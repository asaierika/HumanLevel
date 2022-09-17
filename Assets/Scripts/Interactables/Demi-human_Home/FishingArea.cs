using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the area at which Kizuna can fish
public class FishingArea : Interactable
{
    public Item phonenixFish;
    public Item fishingRod;
    public Conversation convo;
    public GameEvent startFishing;
    public GameEvent endFishing;
    private static bool isFishing;
    private static bool isFished;

    void Update()
    {
        if (isFished)
        this.enabled = false;

        if (!Inventory.instance.Contains(fishingRod))
        return;

        TryInteract();
    }

     public override void Interact()
    {
        isFishing = true;
        StartCoroutine(checkIfHaveFish());
    }

    IEnumerator checkIfHaveFish() 
    {
        // checks if phoenix fish is in the inventory,
        // wait for some time for the fish to be added 
        // to the inventory before checking. Since the player
        // may trigger the collider of the fish and this object
        // at the same time.
        yield return new WaitForSeconds(0.1f);
       
        if (Inventory.instance.Contains(phonenixFish))
        {
            isFishing = false;
            isFished = true;
        } 
        else
        {
            DialogueManager.StartConversation(convo);
            startFishing.TriggerEvent();
            isFishing = false;
        } 
    }
}