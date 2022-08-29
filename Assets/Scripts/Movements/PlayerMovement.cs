using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D body;
    // Update is called once per frame
    void Start() {
        body = GetComponent<Rigidbody2D>();
        if (body == null) {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }
    void FixedUpdate()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal") * playerSpeed;
        float verticalSpeed = Input.GetAxis("Vertical") * playerSpeed;
        // body.velocity = new Vector2(horizontalSpeed, verticalSpeed);
        body.MovePosition(body.position + Time.deltaTime * new Vector2(horizontalSpeed, verticalSpeed));
    }
}
