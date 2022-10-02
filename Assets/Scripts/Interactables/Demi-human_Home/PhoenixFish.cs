using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoenixFish : Follower_simple
{
    public GameObject phoenixFish;
    public GameEvent startFishing;
    public Conversation convo;
    public Item phoenixFishItem;
    public Item fishingRod;
    private SpriteRenderer spriteR;

    void Start() 
    {
        Initialize();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Trace();
        TryInteract();
    }

    void FixedUpdate() {
        if (playerInRange)
        return;
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
        if (!Inventory.instance.Contains(fishingRod))
        return;

        DialogueManager.StartConversation(convo);
        startFishing.TriggerEvent();
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
