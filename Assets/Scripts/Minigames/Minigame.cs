using UnityEngine;
using UnityEngine.Events;
using GameInterfaces;

/**
* The class represents the mini game players will be playing 
* which takes the form of a popup. Script should be at the root of the gameobject.
*/
public abstract class Minigame : MonoBehaviour, ICompletable
{
    public UnityEvent<object> onGameCompleted;
    public ValidPlayerState.Who gameOwner {get; set; }
    public abstract float GetProgress();
    // All minigames should handle forced exit from player.
    public abstract void OnKeyboardExit();

    void OnEnable() {
        gameOwner = StateManager.instance.GetCurrentIdentity();
    }

    void OnDisable() {
        gameOwner = ValidPlayerState.Who.NONE;
    }
}
