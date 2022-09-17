using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // true when the player triggers the collider of the interactable object 
    // false when the player exits
    public bool playerInRange;

    public void TryInteract()
    {
        
        if (GameManager.instance.foxFrozen)
            // when the fox is frozen, eg inventory is open or in dialogue,
            // the player cannot interact with interactable objects 
            return;

        // when the player is in the range of the interactable object and
        // at the same time the player press "Z", Interact() is called
        if (Input.GetKeyDown(KeyCode.Z) && playerInRange) {
            Interact();
        }
    }

    public virtual void Interact()
    { }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player in range");
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
