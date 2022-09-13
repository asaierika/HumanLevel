using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixFish : InteractableFollower
{
    public GameObject phoenixFish;

    public Conversation convo;

    public Item phoenixFishItem;

    private SpriteRenderer spriteR;

    void Start() 
    {
        Initialize();
        spriteR = gameObject.GetComponent<SpriteRenderer>();

        GameEvents.instance.onEnterSpiritMode += Show;
        GameEvents.instance.onExitSpiritMode -= Hide;
    }

    void Update()
    {
        Trace();
        TryInteract();
    }

    void FixedUpdate() {
        if (spriteR.enabled)
        {
        Move();
        Change();
        }
    }

    protected void Change() {
        if (transform.position.x >= player.position.x)
        transform.localScale = new Vector3(1, 1, 1);
        else 
        transform.localScale = new Vector3(-1, 1, 1);    
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
