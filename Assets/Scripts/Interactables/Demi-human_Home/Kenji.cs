using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kenji : Interactable
{
    private SpriteRenderer spriteR;
    public Sprite kenjiStanding;
    public Item fishingRod;
    public static bool talked1, talked2;
    public Conversation convo1, convo2, convo3, convo4, convo5;
    public Item phoenixFish;

    void Start() {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        TryInteract();
        if (talked1)
        {
            spriteR.sprite = kenjiStanding;
        }
    }

    public override void Interact()
    {
        if (!Rabbit.talked)
        {
            DialogueManager.StartConversation(convo1);
            return;
        } 
        
        if (!talked1) 
        {
            DialogueManager.StartConversation(convo2);
            talked1 = true;
            return;
        }

        if (!talked2)
        {
            DialogueManager.StartConversation(convo3);
            talked2 = true;
            Inventory.instance.Add(fishingRod);
            return;
        }

        if (!Inventory.instance.Contains(phoenixFish))
        {
            DialogueManager.StartConversation(convo4);
            return;
        }
        Inventory.instance.Remove(fishingRod);
        Inventory.instance.Remove(phoenixFish);
        DialogueManager.StartConversation(convo5);
    }
}
