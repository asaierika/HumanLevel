using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSeller : Interactable
{
    public static bool sawCat, givenFish;
    public Item fish;
    public Conversation convo1, convo2;

    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        if (givenFish)
        {
            DialogueManager.StartConversation(convo1);
            return;
        }
        if (sawCat && Eri.talked)
        {
            DialogueManager.StartConversation(convo2);
            Inventory.instance.Add(fish);
            givenFish = true;
            return;
        }

        DialogueManager.StartConversation(convo1);
    }
}
