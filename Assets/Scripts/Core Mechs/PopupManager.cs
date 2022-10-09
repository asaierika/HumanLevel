using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public SwitchCharacter.Who identity;
    public GameEvent terminatingSignal;
    public GameObject popup;
    public bool hasPopup;
    public CharacterInteractableManager interactableManager;

    void Update() {
        if (hasPopup && Input.GetKeyDown(KeyCode.Escape)) {
            if (terminatingSignal != null) {
                // If there are tasks to be completed before the popup is closed
                terminatingSignal.TriggerEvent();
            } else {
                ClosePopup();
            }
        }
    }
    // One player can only have one popup active at a given moment
    public void ClosePopup() {
        popup.SetActive(false);
        this.popup = null;
        this.hasPopup = false;
        this.terminatingSignal = null;
        interactableManager.RemovePossession(identity, popup);
    }

    public void SetActivePopup(GameObject popup, params GameEvent[] signals) {
        this.popup = popup;
        this.hasPopup = true;
        if (signals.Length > 0) {
            this.terminatingSignal = signals[0];
        }
    }
}
