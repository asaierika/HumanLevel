using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Each playable character will have their own popup manager.
*/
public class PopupManager : MonoBehaviour
{
    // public CharacterSwitcher.Who owner;
    // public GameEvent terminatingSignal;
    // public GameObject popup;
    // public bool hasPopup;
    // public CharacterInteractableManager interactableManager;

    // void Update() {
    //     if (CharacterSwitcher.instance.identity == owner && hasPopup && Input.GetKeyDown(KeyCode.Escape)) {
    //         if (terminatingSignal != null) {
    //             // If there are tasks to be completed before the popup is closed
    //             terminatingSignal.TriggerEvent();
    //         } else {
    //             ClosePopup();
    //         }
    //     }
    // }
    // // One player can only have one popup active at a given moment
    // public void ClosePopup() {
    //     popup.SetActive(false);
    //     interactableManager.RemovePossession(owner, popup.GetComponent<MiniGame>());
    //     this.popup = null;
    //     this.hasPopup = false;
    //     this.terminatingSignal = null;
    // }

    // public void SetActivePopup(GameObject popup, params GameEvent[] signals) {
    //     this.popup = popup;
    //     this.hasPopup = true;
    //     if (signals.Length > 0) {
    //         this.terminatingSignal = signals[0];
    //     }
    // }
}
