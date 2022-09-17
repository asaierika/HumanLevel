using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1f; 
    private Rigidbody2D rb;
    private Vector2 movement;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        TryInteract();
        Trace();
    }

    void FixedUpdate() {
        Move();
    }

    protected void Initialize() {
         rb = this.GetComponent<Rigidbody2D>();
    }

    protected void Change() {
        // specifies how the follower object changes its
        // transform when in different relative position
        // to the player 
    }

    protected void Trace() {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    protected void Move() {
        rb.MovePosition((Vector2) transform.position + (movement * moveSpeed * Time.deltaTime));
    }

    // methods from Interactable class
    public void TryInteract()
    {
        
        if (GameManager.instance.foxFrozen)
            // when the fox is frozen, eg inventory is open or in dialogue,
            // the player cannot interact with interactable objects 
            return;

        // when the player is in the range of the interactable object and
        // at the same time the player press "Z", Interact() is called
        if (Input.GetKeyDown(KeyCode.Z) && playerInRange) {
            Debug.Log("Interact");
            Interact();
        }
    }

    public virtual void Interact(){}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
