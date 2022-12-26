using UnityEngine;

/**
* Represents gameobjects that will start a mini game when it has been interacted with by 
* the player.
*/
// FIXME: If character and player component not on the same object will cause NullReferenceException
public class InteractableWithGame : Interactable
{
    // public GameEvent openMiniGame;
    // public GameEvent[] popupOpen;
    public MinigameManager.MinigameID gameID;
    // public CharacterInteractableManager interactableManager;
    public CharacterSwitcher.Who whoTriggered;

    public override void Interact() {
        // Debug.Log("Player engaged game interactable " + name);
        // interactableManager.AddPossession(whoTriggered, game);
        // game.SetOwner(whoTriggered);
        // openMiniGame.TriggerEvent();
        // if (whoTriggered == CharacterSwitcher.Who.Kizuna) {
        //     popupOpen[0].TriggerEvent();
        // } else {
        //     popupOpen[1].TriggerEvent();
        // }
        MinigameManager.instance.StartMinigame(gameID, CharacterSwitcher.instance.identity);
    }


    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player")) {
    //         whoTriggered = collision.gameObject.GetComponent<Character>().identity;
    //         playerInRange = true;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player") &&
    //             collision.gameObject.GetComponent<Character>().IsActive) {
    //         whoTriggered = CharacterSwitcher.Who.None;
    //         playerInRange = false;
    //     }
    // }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     // Debug.Log("Collision with " + collision.gameObject.name);
    //     if (collision.gameObject.CompareTag("Player") &&
    //             collision.gameObject.GetComponent<Character>().IsActive) {
    //         whoTriggered = collision.gameObject.GetComponent<Character>().identity;
    //         playerInRange = true;
    //     }
    // }

    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player") &&
    //             collision.gameObject.GetComponent<Character>().IsActive) {
    //         whoTriggered = CharacterSwitcher.Who.None;
    //         playerInRange = false;
    //     }
    // }
} 
