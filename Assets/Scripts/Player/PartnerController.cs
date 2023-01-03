using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerController : CharacterController
{
    // NOTE: More specific behviour > generic behaviours. e.g. Partner minigame resume > switch to partner to ensure movement remains disabled.
    // The above is enforced by implementation where the event PARTNER_MINIGAME_RESUME is triggered in @method TryResumePartnerMinigame() when 
    // SWITCH_TO_PARTNER is triggered. Is there a better way to enforce this rule?
    void OnEnable() {
        EventManager.StartListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, TryRestoreMovementWrapper);
        EventManager.StartListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, FreezeMovementWrapper);
        EventManager.StartListening(EventManager.Event.PARTNER_MINIGAME_START, FreezeMovementWrapper);
        EventManager.StartListening(EventManager.Event.PARTNER_MINIGAME_END, TryRestoreMovementWrapper);
    }

    void OnDisable() {
        EventManager.StopListening(EventManager.Event.SWITCH_TO_PARTNER_DEMI, TryRestoreMovementWrapper);
        EventManager.StopListening(EventManager.Event.SWITCH_TO_KIZUNA_DEMI, FreezeMovementWrapper);
        EventManager.StopListening(EventManager.Event.PARTNER_MINIGAME_START, FreezeMovementWrapper);
        EventManager.StopListening(EventManager.Event.PARTNER_MINIGAME_END, TryRestoreMovementWrapper);
    }
}
