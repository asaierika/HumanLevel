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
    private Animator animator;

    void Start() {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        inventory = GameManager.instance.inventory;
        animator = animator == null ? GetComponent<Animator>() : animator;
    }

    void Update()
    {
        TryInteract();
        if (talked1)
        {
            animator.SetBool("fishing", false);
        }
    }

    public override void Interact()
    {
        if (!Rabbit.talked)
        {
            DialogueManager.instance.StartConversation(convo1);
            return;
        } 
        
        if (!talked1) 
        {
            DialogueManager.instance.StartConversation(convo2);
            talked1 = true;
            return;
        }

        if (!talked2)
        {
            DialogueManager.instance.StartConversation(convo3);
            talked2 = true;
            inventory.Add(fishingRod);
            return;
        }

        if (!inventory.Contains(phoenixFish))
        {
            DialogueManager.instance.StartConversation(convo4);
            return;
        }
        inventory.UseItem(fishingRod);
        inventory.UseItem(phoenixFish);
        DialogueManager.instance.StartConversation(convo5);
    }
}
