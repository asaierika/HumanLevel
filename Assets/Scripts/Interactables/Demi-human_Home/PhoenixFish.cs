using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixFish : Interactable
{
    public GameObject phoenixFish;

    public Conversation convo;

    public Item phoenixFishItem;

    private SpriteRenderer spriteR;

    void Start() 
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();

        GameEvents.instance.onEnterSpiritMode += Show;
        GameEvents.instance.onExitSpiritMode -= Hide;
    }
    void Update()
    {
        TryInteract();
    }

    public override void Interact()
    {
        DialogueManager.StartConversation(convo);
        Inventory.instance.Add(phoenixFishItem);
        phoenixFish.SetActive(false);
    }

    public void Show()
    {
        spriteR.enabled = true;
    }

    public void Hide()
    {
        spriteR.enabled = false;
    }
}
