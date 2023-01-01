using UnityEngine;

public class PhoenixFish : SimpleFollower
{
    private SpriteRenderer spriteR;
    private Inventory inventory;
    public Item phoenixFishItem;
    public Item fishingRod;


    void Start() 
    {
        Initialize();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        inventory = GameManager.instance.inventory;
    }

    void Update()
    {
        Trace();
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
    public void UseFishingRod()
    {
        if (!playerInRange)
        return;
        
        inventory.Add(phoenixFishItem);
        gameObject.SetActive(false);
    }

    protected override void Change() {
        if (transform.position.x >= player.position.x)
        transform.localScale = new Vector3(1, 1, 1);
        else 
        transform.localScale = new Vector3(-1, 1, 1);    
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
