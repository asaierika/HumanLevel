using UnityEngine;

/**
* Represents gameobjects that will start a mini game when it has been interacted with by 
* the player.
*/
public class InteractableWithGame : Interactable
{
    public MinigameManager.MinigameID gameID;
    
    public override void Interact() {
        MinigameManager.instance.StartMinigame(gameID);
    }
} 
