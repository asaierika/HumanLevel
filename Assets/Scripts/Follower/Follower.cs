using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Follower : MonoBehaviour
{
    protected Transform player;
    public float moveSpeed = 1f; 
    protected Rigidbody2D rb;
    protected Vector2 movement;
    private bool isColliding;
    public bool playerInRange;
    protected enum Direction { Horizontal, Vertical }
    protected Direction direction = Direction.Horizontal;
    protected RaycastHit2D hitX;
    protected RaycastHit2D hitY;
    protected BoxCollider2D boxCollider;

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
        boxCollider = this.GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("Player").transform;

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

        hitX = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(movement.x, 0), 
            0.01f, LayerMask.GetMask("Blocking"));
        hitY = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, movement.y), 
            0.01f, LayerMask.GetMask("Blocking"));
        
        if (hitX.collider != null)
        {
            this.direction = Direction.Vertical;
            return;
        }
        if (hitY.collider != null)
        {
            this.direction = Direction.Horizontal;
            return;
        }

        if (Math.Abs(movement.x) < Math.Abs(movement.y))
        {
            this.direction = Direction.Vertical;
        }
        else 
        {
            this.direction = Direction.Horizontal;
        }
    }


    protected void Move() {
        if (this.direction == Direction.Horizontal) 
        {
            if (movement.x > 0)
            {
                rb.MovePosition((Vector2) transform.position 
                    + new Vector2(1 * moveSpeed * Time.deltaTime, 0));
            }
            else if (movement.x < 0)
            {
                rb.MovePosition((Vector2) transform.position 
                    + new Vector2(-1 * moveSpeed * Time.deltaTime, 0));
            }
            else
            {
                return;
            }
        } 
        else if (this.direction == Direction.Vertical)
        {
            if (movement.y > 0)
            {
                rb.MovePosition((Vector2) transform.position 
                    + new Vector2(0, 1 * moveSpeed * Time.deltaTime));
            }
            else if (movement.y < 0)
            {
                rb.MovePosition((Vector2) transform.position 
                    + new Vector2(0, -1 * moveSpeed * Time.deltaTime));
            } 
            else 
            {
                return;
            }
        }
        //rb.MovePosition((Vector2) transform.position + (movement * moveSpeed * Time.deltaTime));
    }
    

    // methods from Interactable class
    // ignore if not using interactable
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
