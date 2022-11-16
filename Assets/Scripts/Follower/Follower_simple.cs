using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// simple follower that allows sliding
public class Follower_simple : Interactable
{
    protected Transform player;
    public float moveSpeed = 1f; 
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
        TryInteract();
        Trace();
    }

    void FixedUpdate() {
        Move();
    }

    protected void Initialize() {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

    }

    protected virtual void Change() {
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
}
