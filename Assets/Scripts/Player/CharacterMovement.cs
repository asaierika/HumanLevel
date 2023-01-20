using UnityEngine;
using System;

// TESTCODE: All null reference guards should eventually be removed, as every playable character are expected to be animated.
// FIXME: Adopt abstract movement then concrete KizunaMovement, TezuoMovement etc. to accomodate for character differences.
public abstract class CharacterMovement : MonoBehaviour
{
    // Whether player transition between scenes should be simulated as continuous. e.g. Eri chase Kizuna in castle scene
    public static bool inContinuousLocations;
    public static bool playerFrozen = false;
    public float moveSpeed = 1f;
    public Rigidbody2D rb;
    public Animator animator;
    [SerializeField]
    protected bool characterFrozen;

    public static void AlterLocationType(bool isContinuousType) {
        inContinuousLocations = isContinuousType;
    }

    void OnEnable() {
        UiStatus.onOpenUI += Freeze;
        UiStatus.onCloseUI += TryRestore;
        if (CameraManager.main != null) {
            // OnEnabled is called before awake of another gameobject
            CameraManager.main.onCameraTransit += Freeze;
            CameraManager.main.onCameraTransitComplete += TryRestore;
        }
    }

    void OnDisable() {
        UiStatus.onOpenUI -= Freeze;
        UiStatus.onCloseUI -= TryRestore;
        CameraManager.main.onCameraTransit -= Freeze;
        CameraManager.main.onCameraTransitComplete -= TryRestore;
    }

    void Start() {   
        rb = rb == null ? GetComponent<Rigidbody2D>() : rb;
        animator = animator == null ? GetComponent<Animator>() : animator;  
        CameraManager.main.onCameraTransit += Freeze;
        CameraManager.main.onCameraTransitComplete += TryRestore;
    }

    void FixedUpdate()
    {    
        // Logically, when playerFrozen characterFrozen == true, but
        // for convenience both might not evaluate to true simultaneously
        // hence the condition clause below.
        if (playerFrozen || characterFrozen) return;
        
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

    private void TryMove(Vector2 direction) {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }   

    public void Freeze() {
        characterFrozen = true;
        if (animator != null) animator.SetBool("moving", false);
    }

    // Re-evaluate whether movement should be frozen.
    public abstract void TryRestore();
}
