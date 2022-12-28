using UnityEngine;

public class PhoenixFish_SimpleFollower : SimpleFollower
{
    private SpriteRenderer spriteR;

    void Start() 
    {
        Initialize();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
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
