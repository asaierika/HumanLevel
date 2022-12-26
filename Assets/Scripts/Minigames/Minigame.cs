using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameInterfaces;

/**
* The class represents the mini game players will be playing 
* which takes the form of a popup.
*/
public abstract class Minigame : MonoBehaviour, ICompletable
{
    public UnityEvent<object> OnGameCompleted;
    public CharacterSwitcher.Who gameOwner {get; set; }
    public abstract float GetProgress();
    // All minigames should handle forced exit from player.
    public abstract void OnKeyboardExit();
//     public CharacterSwitcher.Who owner;
//     // Use to make game invisible when switching to another character.
//     public SpriteRenderer[] renderers;
//     // One manager for Kizuna, one for Partner
//     // Order important
//     public PopupManager[] popupManagers;
//     public GameEvent terminateSignal;
//     // Used to determine the active status of this object when player switches between characters.
//     // Tied together with CharacterInteractableManager. 
//     public bool isCorrectCharacter = true;

//     // Will be called even when switching characters, popup value will be set
//     // again to correspond to same minigame
//     void OnEnable() {
//         Debug.Log(owner);
//         if (terminateSignal == null) {
//             if (owner == CharacterSwitcher.Who.Kizuna) {
//                 popupManagers[0].SetActivePopup(gameObject);
//             } else if (owner == CharacterSwitcher.Who.Partner) {
//                 popupManagers[1].SetActivePopup(gameObject);
//             } else {
//                 throw new UnityException("Invalid owner for minigame " + name);
//             }
//         } else {
//             if (owner == CharacterSwitcher.Who.Kizuna) {
//                 popupManagers[0].SetActivePopup(gameObject, terminateSignal);
//             } else if (owner == CharacterSwitcher.Who.Partner) {
//                 popupManagers[1].SetActivePopup(gameObject, terminateSignal);
//             } else {
//                 throw new UnityException("Invalid owner for minigame " + name);
//             }
//         }
//     }

//     // void OnDisable() {
//     //     this.owner = CharacterSwitcher.Who.None;
//     // }

//     // Always called before the mini game popup is enabled
//     public void SetOwner(CharacterSwitcher.Who owner) {
//         this.owner = owner;
//     }
}
