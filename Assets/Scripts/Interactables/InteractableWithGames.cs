using UnityEngine;

/**
* Represents gameobjects that will start a mini game when it has been interacted with by 
* the player.
*/
public class InteractableWithGames : Interactable
{
    public GameEvent openMiniGame;
    public GameEvent[] popupOpen;
    public MiniGame game;
    public CharacterInteractableManager interactableManager;
    public SwitchCharacter.Who whoTriggered;
    
    void Update() {
        TryInteract();
    }

    public override void Interact() {
        // Debug.Log("Player engaged game interactable " + name);
        interactableManager.AddPossession(whoTriggered, game);
        game.SetOwner(whoTriggered);
        openMiniGame.TriggerEvent();
        if (whoTriggered == SwitchCharacter.Who.Kizuna) {
            popupOpen[0].TriggerEvent();
        } else {
            popupOpen[1].TriggerEvent();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Character>().IsActive) {
            whoTriggered = collision.gameObject.GetComponent<Character>().identity;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Character>().IsActive) {
            whoTriggered = SwitchCharacter.Who.None;
            playerInRange = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collision with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Character>().IsActive) {
            whoTriggered = collision.gameObject.GetComponent<Character>().identity;
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&
                collision.gameObject.GetComponent<Character>().IsActive) {
            whoTriggered = SwitchCharacter.Who.None;
            playerInRange = false;
        }
    }
} 
