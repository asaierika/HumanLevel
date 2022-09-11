using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KizunaMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public VectorValue startingPosition;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        if (GameManager.instance.foxInitialised)
        transform.position = startingPosition.initialValue;
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        GameEvents.instance.onOpenUI += FreezeMovement;
        GameEvents.instance.onCloseUI += RestoreMovement;        
    }

    private void FixedUpdate()
    {    
        if (GameManager.instance.foxFrozen)
        {
            return;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0)
        {
            TryMove(new Vector2(x, y));
            animator.SetFloat("moveX", x);
            animator.SetFloat("moveY", y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void TryMove(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }   

    public void FreezeMovement() {
        GameManager.instance.foxFrozen = true;
    }

    public void RestoreMovement() {
        GameManager.instance.foxFrozen = false;
    }
}
