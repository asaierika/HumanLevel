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
    private RaycastHit2D hitX;
    private RaycastHit2D hitY;
    private BoxCollider2D boxCollider;
    public Vector2 raycastPosition;

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

        if (boxCollider == null) {
            boxCollider = GetComponent<BoxCollider2D>();
        }

        GameEvents.instance.onOpenUI += FreezeMovement;
        GameEvents.instance.onCloseUI += RestoreMovement;        
    }

    private void FixedUpdate()
    {    
        if (GameManager.instance.playerFrozen)
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
            if (animator != null) 
            animator.SetBool("moving", false);
        }
        
    }

    private void TryMove(Vector2 movement)
    {
        /*
        Vector2 boxPosition = new Vector2(transform.position.x + boxCollider.offset.x,
            transform.position.y + boxCollider.offset.y);
        hitX = Physics2D.BoxCast(boxPosition, boxCollider.size, 0, new Vector2(movement.x, 0), 
            Mathf.Abs(movement.x * Time.deltaTime * moveSpeed), LayerMask.GetMask("Game Object", "Blocking"));
        hitY = Physics2D.BoxCast(boxPosition, boxCollider.size, 0, new Vector2(0, movement.y), 
            Mathf.Abs(movement.y * Time.deltaTime * moveSpeed), LayerMask.GetMask("Game Object", "Blocking"));
        if (hitX.collider != null)
        movement.x = 0f;

        if (hitY.collider != null)
        movement.y = 0f;
        */
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }   

    public void FreezeMovement() {
        GameManager.instance.playerFrozen = true;
        if (animator != null) animator.SetBool("moving", false);
    }

    public void RestoreMovement() {
        Debug.Log("restore movement");
        GameManager.instance.playerFrozen = false;
    }
}
