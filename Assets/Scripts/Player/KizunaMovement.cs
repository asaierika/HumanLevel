using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KizunaMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public VectorValue startingPosition;
    public Rigidbody2D rb;
    public Animator animator;

    private void Start()
    {
        if (GameManager.instance.foxInitialised)
        transform.position = startingPosition.initialValue;
        
        if (rb == null) {
            rb = GetComponent<Rigidbody2D>();
        }

        if (animator == null) {
            animator = GetComponent<Animator>();
        }

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
        GameManager.instance.foxFrozen = true;
        if (animator != null) animator.SetBool("moving", false);
    }

    public void RestoreMovement() {
        Debug.Log("restore movement");
        GameManager.instance.foxFrozen = false;
    }
}
