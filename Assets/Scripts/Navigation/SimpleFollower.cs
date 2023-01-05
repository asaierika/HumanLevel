using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// simple follower that allows sliding
public abstract class SimpleFollower : MonoBehaviour
{
    public float moveSpeed = 1f; 
    public bool playerInRange;
    // Allows player to be modified in the inspector
    // as the followed game object can be the spirit or partner.
    // If not specified, the followed game object would be the one
    // with a "Player" tag.
    public Transform player;
    protected Rigidbody2D rb;
    protected Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        Trace();
    }

    void FixedUpdate() {
        Move();
    }

    protected void Initialize() {
        rb = this.GetComponent<Rigidbody2D>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }

    // specifies how the follower object changes its
    // transform when in different relative position
    // to the player 
    protected abstract void Change();

    protected void Trace() {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
    }

    protected void Move() {
        rb.MovePosition((Vector2) transform.position + (movement * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(player.tag))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(player.tag))
        {
            playerInRange = false;
        } 
    }
}
