using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat2 : Interactable
{
    public Conversation convo1, convo2, convo3;
    public Item fish, hairPin;
    public static bool fed;

    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        if (fed)
        {
            DialogueManager.StartConversation(convo3);            
            return;
        }
        if (Inventory.instance.Contains(fish))
        {
            DialogueManager.StartConversation(convo2);
            fed = true;
            Inventory.instance.Add(hairPin);
            Inventory.instance.Remove(fish);
            return;
        }

        DialogueManager.StartConversation(convo1);
        FishSeller.sawCat = true;
    }
}
