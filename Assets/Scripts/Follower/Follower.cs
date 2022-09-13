using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1f; 
    private Rigidbody2D rb;
    private Vector2 movement;

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

    void FixedUPdate() {
        Move();
        Change();
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
}
