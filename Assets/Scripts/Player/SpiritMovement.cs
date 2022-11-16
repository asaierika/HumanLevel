using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpiritMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public VectorValue startingPosition;
    public Rigidbody2D rb;
    public Animator animator;
    public UiStatus uiStatus;

    private void Start()
    {
        if (GameManager.instance.playerInitialised)
        transform.position = startingPosition.initialValue;
        
        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }

        if (animator == null) {
            animator = GetComponent<Animator>();
        }

        uiStatus.onOpenUI += FreezeMovement;
        uiStatus.onCloseUI += RestoreMovement;        
    }

    private void FixedUpdate()
    {    
        if (GameManager.instance.playerFrozen)
        {
            //return;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

  
        if (x != 0 || y != 0)
        {
            TryMove(new Vector2(x, y));
            // The condition clause should eventually be removed
            // as all PCs should have an animator
            if (animator != null) {
                animator.SetFloat("moveX", x);
                animator.SetFloat("moveY", y);
                animator.SetBool("moving", true);
            }
        } else {
            if (animator != null) animator.SetBool("moving", false);
        }
        
    }

    private void TryMove(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }   

    public void FreezeMovement() {
        GameManager.instance.playerFrozen = true;
        if (animator != null) animator.SetBool("moving", false);
    }

    public void RestoreMovement() {
        GameManager.instance.playerFrozen = false;
    }
}

