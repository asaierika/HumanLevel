using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KizunaController : CharacterController
{
    public GameObject spirit;
    void OnEnable() {
        // No need deactivate spirit for switch_to_partner event as trznsition from spirit to partner is not possible
        EventManager.StartListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, FreezeMovementWrapper);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, TryRestoreMovementWrapper);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, DeactivateSpirit);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, FreezeMovementWrapper);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, ActivateSpirit);
        EventManager.StartListening(EventManager.Event.KIZUNA_MINIGAME_START, FreezeMovementWrapper);
        EventManager.StartListening(EventManager.Event.KIZUNA_MINIGAME_END, TryRestoreMovementWrapper);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, FreezeMovementWrapper);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, TryRestoreMovementWrapper);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, DeactivateSpirit);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, FreezeMovementWrapper);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_SPIRIT, ActivateSpirit);
        EventManager.StopListening(EventManager.Event.KIZUNA_MINIGAME_START, FreezeMovementWrapper);
        EventManager.StopListening(EventManager.Event.KIZUNA_MINIGAME_END, TryRestoreMovementWrapper);
    }

    void ActivateSpirit(object o = null) {
        spirit.SetActive(true);
    }

    void DeactivateSpirit(object o = null) {
        spirit.SetActive(false);
    }
}
