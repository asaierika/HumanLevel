using UnityEngine;

public class PhoenixFish : Follower_simple
{
    public GameObject phoenixFish;
    public GameEvent startFishing;
    private Inventory inventory;
    public Item phoenixFishItem;
    public Item fishingRod;
    private SpriteRenderer spriteR;

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

    protected override void Change() {
        if (transform.position.x >= player.position.x)
        transform.localScale = new Vector3(1, 1, 1);
        else 
        transform.localScale = new Vector3(-1, 1, 1);    
    }

    public override void Interact()
    {
        if (!inventory.Contains(fishingRod))
        return;

        inventory.Add(phoenixFishItem);
        phoenixFish.SetActive(false);
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
