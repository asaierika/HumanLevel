using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    // true when the player triggers the collider of the interactable object 
    // false when the player exits
    public bool playerInRange;

    void Update() {
        TryInteract();
    }

    public virtual void TryInteract()
    {
        if (CharacterMovement.playerFrozen)
            // when the player is frozen, eg inventory is open or in dialogue,
            // the player cannot interact with interactable objects 
            return;

        // when the player is in the range of the interactable object and
        // at the same time the player press "Z", Interact() is called
        if (InputManager.instance.interactButtonActivated && playerInRange) {
            Interact();
        }
    }

    public abstract void Interact();
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsPlayer(collision.gameObject)) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsPlayer(collision.gameObject)) {
            playerInRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision with " + collision.gameObject.name);
        if (IsPlayer(collision.gameObject)) {
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsPlayer(collision.gameObject)) {
            playerInRange = false;
        }
    }

    private bool IsPlayer(GameObject otherObject) {
        return (otherObject.CompareTag("Player") && StateManager.instance.CurrPlayerState.Identity == ValidPlayerState.Who.KIZUNA)
                || (otherObject.CompareTag("Partner") && StateManager.instance.CurrPlayerState.Identity == ValidPlayerState.Who.PARTNER);
    }
}
