using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kenji : Conversable
{
    private SpriteRenderer spriteR;
    public Sprite kenjiStanding;
    public Item fishingRod;
    public static bool talked1, talked2;
    public Conversation convo1, convo2, convo3, convo4, convo5;
    public Item phoenixFish;
    public Inventory inventory;

    void Start() {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        inventory = GameManager.instance.inventory;
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
            dialogueManager.StartConversation(convo1);
            return;
        } 
        
        if (!talked1) 
        {
            dialogueManager.StartConversation(convo2);
            talked1 = true;
            return;
        }

        if (!talked2)
        {
            dialogueManager.StartConversation(convo3);
            talked2 = true;
            inventory.Add(fishingRod);
            return;
        }

        if (!inventory.Contains(phoenixFish))
        {
            dialogueManager.StartConversation(convo4);
            return;
        }
        inventory.Remove(fishingRod);
        inventory.Remove(phoenixFish);
        dialogueManager.StartConversation(convo5);
    }
}
