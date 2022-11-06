using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public VectorValue startingPosition;
    public Rigidbody2D rb;
    public Animator animator;
    public static bool characterFrozen;

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

        GameEvents.instance.onOpenUI += FreezeCharacterMovement;
        GameEvents.instance.onCloseUI += RestoreCharacterMovement;        
    }

    private void FixedUpdate()
    {    
        // Logically, when playerFrozen characterFrozen == true, but
        // for convenience both might not evaluate to true simultaneously
        // hence the condition clause below.
        if (characterFrozen || GameManager.instance.playerFrozen)
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

    // Called when all characters the user could have control of should be frozen.
    // eg. Inventory, dialogue
    public void FreezeMovement() {
        GameManager.instance.playerFrozen = true;
        if (animator != null) animator.SetBool("moving", false);
    }

    // Converse of FreezeMovement
    public void RestoreMovement() {
        GameManager.instance.playerFrozen = false;
    }

    public void FreezeCharacterMovement() {
        characterFrozen = true;
        if (animator != null) animator.SetBool("moving", false);
    }

    public void RestoreCharacterMovement() {
        characterFrozen = false;
    }
}

