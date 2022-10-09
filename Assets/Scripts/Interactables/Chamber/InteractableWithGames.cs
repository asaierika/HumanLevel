using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWithGames : Interactable
{
    public GameEvent openMiniGame;
    public GameEvent popupOpen;
    public MiniGame game;
    public CharacterInteractableManager interactableManager;
    public SwitchCharacter.Who whoTriggered;
    
    void Update() {
        TryInteract();
    }

    public override void Interact() {
        Debug.Log("Player engaged table");
        interactableManager.AddPossession(whoTriggered, game.gameObject);
        game.SetOwner(whoTriggered);
        openMiniGame.TriggerEvent();
        popupOpen.TriggerEvent();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Playable>().IsActive) {
            whoTriggered = collision.gameObject.GetComponent<Playable>().identity;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Playable>().IsActive) {
            whoTriggered = SwitchCharacter.Who.None;
            playerInRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Playable>().IsActive) {
            whoTriggered = collision.gameObject.GetComponent<Playable>().identity;
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Playable>().IsActive) {
            whoTriggered = SwitchCharacter.Who.None;
            playerInRange = false;
        }
    }
} 
