using UnityEngine;

public class PhoenixFish : SimpleFollower
{
    private SpriteRenderer spriteR;
    private Inventory inventory;
    public Item phoenixFishItem;
    public Item fishingRod;


    void OnEnable() {
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, Show);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, Hide);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, Show);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, Hide);
    }

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


    public void Show(object o = null)
    {
        spriteR.enabled = true;
    }

    public void Hide(object o = null)
    {
        spriteR.enabled = false;
    }
}
