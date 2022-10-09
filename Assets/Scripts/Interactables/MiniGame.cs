using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public SwitchCharacter.Who owner;

    // One manager for Kizuna, one for Partner
    // Order important
    public PopupManager[] popupManagers;
    public GameEvent terminateSignal;

    // Will be called even when switching characters, popup value will be set
    // again to correspond to same minigame
    void OnEnable() {
        Debug.Log(owner);
        if (terminateSignal == null) {
            if (owner == SwitchCharacter.Who.Kizuna) {
                popupManagers[0].SetActivePopup(gameObject);
            } else if (owner == SwitchCharacter.Who.Partner) {
                popupManagers[1].SetActivePopup(gameObject);
            } else {
                throw new UnityException("Fuck the world and coding");
            }
        } else {
            if (owner == SwitchCharacter.Who.Kizuna) {
                popupManagers[0].SetActivePopup(gameObject, terminateSignal);
            } else if (owner == SwitchCharacter.Who.Partner) {
                popupManagers[1].SetActivePopup(gameObject, terminateSignal);
            } else {
                throw new UnityException("Fuck the world and coding");
            }
        }
    }

    // void OnDisable() {
    //     this.owner = SwitchCharacter.Who.None;
    // }

    // Always called before the mini game popup is enabled
    public void SetOwner(SwitchCharacter.Who owner) {
        this.owner = owner;
    }
}
