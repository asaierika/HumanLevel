using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Follower : MonoBehaviour
{
    protected Transform player;
    public float moveSpeed = 1f; 
    protected Vector2 movement;
    public bool playerInRange;
    protected enum Direction { Horizontal, Vertical }
    protected Direction direction = Direction.Horizontal;
    public VectorValue startingPosition;

    protected Rigidbody2D rb;
    protected RaycastHit2D hitX;
    protected RaycastHit2D hitY;
    protected BoxCollider2D boxCollider;
    protected Animator animator;
   

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        Trace();
    }

    protected virtual void FixedUpdate() {
        if (!FollowingManager.instance.isFollowing)
        return;

        if (playerInRange)
        {
            animator.SetBool("moving", false);
            FollowingManager.instance.isFollowing = false;
            Found();
            return;
        }
        Move();
        Change();
    }

    protected virtual void Initialize() {
        rb = this.GetComponent<Rigidbody2D>();
        boxCollider = this.GetComponent<BoxCollider2D>();
        animator = this.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        if (FollowingManager.instance.isFollowing)
        {
            //FollowingManager.instance.instance.SwitchScene(startingPosition.initialValue);
        }
    }

    protected void Trace() {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;

        hitX = Physics2D.BoxCast(transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y, 0f), boxCollider.size, 0, new Vector2(movement.x, 0), 
            0.1f, LayerMask.GetMask("Blocking"));
        hitY = Physics2D.BoxCast(transform.position + new Vector3(boxCollider.offset.x, boxCollider.offset.y, 0f), boxCollider.size, 0, new Vector2(0, movement.y), 
            0.1f, LayerMask.GetMask("Blocking"));
        
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
            if (Mathf.Abs(movement.x) < 0.01f)
            return;

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
            if (Mathf.Abs(movement.y) < 0.01f)
            return;

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

    protected void Change() 
    {
        if (animator == null) 
        return;

        if (this.direction == Direction.Horizontal) 
        {
            animator.SetFloat("moveX", movement.x);
            animator.SetFloat("moveY", 0);
            animator.SetBool("moving", true);
        } 
        else 
        {
            animator.SetFloat("moveY", movement.y);
            animator.SetFloat("moveX", 0);
            animator.SetBool("moving", true);
        }
    }

    protected abstract void Found();

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
