using UnityEngine;

// TESTCODE: All null reference guards should eventually be removed, as every playable character are expected to be animated.
public class PlayerMovement : MonoBehaviour
{
    // Whether player transition between scenes should be simulated as continuous. e.g. Eri chase Kizuna in castle scene
    public static bool inContinuousLocations;
    public static bool PLAYER_FROZEN = false;
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    public Animator animator;
    public bool characterFrozen;

    public static void AlterLocationType(bool isContinuousType) {
        inContinuousLocations = isContinuousType;
    }

    private void Start()
    {   
        rb = rb == null ? GetComponent<Rigidbody2D>() : rb;
        animator = animator == null ? GetComponent<Animator>() : animator;

        UiStatus.onOpenUI += FreezeAllMovement;
        UiStatus.onCloseUI += RestoreAllMovement;        
    }

    private void FixedUpdate()
    {   
        // Logically, when playerFrozen characterFrozen == true, but
        // for convenience both might not evaluate to true simultaneously
        // hence the condition clause below.
        if (characterFrozen || PLAYER_FROZEN) return;
        
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
            if (animator != null) {
                animator.SetBool("moving", false);
            }
        }
        
    }

    private void TryMove(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }   

    // Called when all characters the user could have control of should be frozen.
    // eg. Inventory, dialogue
    public void FreezeAllMovement() {
        Debug.Log("freeze all movement");
        PLAYER_FROZEN = true;
        if (animator != null) {
            animator?.SetBool("moving", false);
        }
    }

    // Converse of FreezeMovement
    public void RestoreAllMovement() {
        Debug.Log("restore all movement");
        PLAYER_FROZEN = false;
    }

    public void FreezeCharacterMovement() {
        characterFrozen = true;
        if (animator != null) {
            animator?.SetBool("moving", false);
        }
    }

    public void RestoreCharacterMovement() {
        characterFrozen = false;
    }
}

