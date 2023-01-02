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
        if (PlayerMovement.PLAYER_FROZEN || DialogueManager.instance.inDialogue)
            // when the player is frozen, eg inventory is open or in dialogue,
            // the player cannot interact with interactable objects 
            return;

        // when the player is in the range of the interactable object and
        // at the same time the player press "Z", Interact() is called
        if (Input.GetKeyDown(KeyCode.Z) && playerInRange && !PlayerMovement.PLAYER_FROZEN) {
            Interact();
        }
    }

    public abstract void Interact();
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
            playerInRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            playerInRange = false;
        }
    }
}
