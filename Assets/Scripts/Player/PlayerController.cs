using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement bodyMovement;
    // The number of locks that is imposed on movement. 
    // E.g. Kizuna minigame start + switch to partner results in two locks that can only be unlocked via minigame end and switch to kizuna.
    // All toggle mechanisms should ensure that reflexive transitions are not possible. There should not be two consecutive occurence of the same event.
    // Else, a dictionary lock must be used instead.
    // NOTE: For character whose default movement is disabled, lock should be set to 1.
    [SerializeField]
    private int movementLocks;
    public ValidPlayerState.Who identity;
    public GameObject spirit = null;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (identity == ValidPlayerState.Who.KIZUNA) {
            // No need deactivate spirit for switch_to_partner event as trznsition from spirit to partner is not possible
            EventManager.StartListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, DisableBodyMovement);
            EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, EnableBodyMovement);
            EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, DeactivateSpirit);
            EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, DisableBodyMovement);
            EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, ActivateSpirit);
            EventManager.StartListening(EventManager.Event.KIZUNA_MINIGAME_START, DisableBodyMovement);
            EventManager.StartListening(EventManager.Event.KIZUNA_MINIGAME_END, EnableBodyMovement);
        } else if (identity == ValidPlayerState.Who.PARTNER) {
            EventManager.StartListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, EnableBodyMovement);
            EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, DisableBodyMovement);
            EventManager.StartListening(EventManager.Event.PARTNER_MINIGAME_START, DisableBodyMovement);
            EventManager.StartListening(EventManager.Event.PARTNER_MINIGAME_END, EnableBodyMovement);
        }
    }

    // Update is called once per frame
    void OnDisable()
    {
        if (identity == ValidPlayerState.Who.PARTNER) {
            EventManager.StopListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, DisableBodyMovement);
            EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, EnableBodyMovement);
            EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, DeactivateSpirit);
            EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, DisableBodyMovement);
            EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, ActivateSpirit);
            EventManager.StopListening(EventManager.Event.KIZUNA_MINIGAME_START, DisableBodyMovement);
            EventManager.StopListening(EventManager.Event.KIZUNA_MINIGAME_END, EnableBodyMovement);
        } else if (identity == ValidPlayerState.Who.PARTNER) {
            EventManager.StopListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, EnableBodyMovement);
            EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, DisableBodyMovement);
            EventManager.StopListening(EventManager.Event.PARTNER_MINIGAME_START, DisableBodyMovement);
            EventManager.StopListening(EventManager.Event.PARTNER_MINIGAME_END, EnableBodyMovement);
        }
    }

    void EnableBodyMovement(object o = null) {
        movementLocks -= 1;
        bodyMovement.enabled = movementLocks == 0;
    }

    void DisableBodyMovement(object o = null) {
        movementLocks += 1;
        bodyMovement.enabled = false;
    }

    void ActivateSpirit(object o = null) {
        spirit.SetActive(true);
    }

    void DeactivateSpirit(object o = null) {
        spirit.SetActive(false);
    }
}
