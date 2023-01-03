using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KizunaMovement : CharacterMovement
{
    public override void TryRestore() {
        Debug.Log("Trying to restore Kizuna movement");
        // Reevaluate whether movement should be frozen.
        characterFrozen = UiStatus.isOpen || (MinigameManager.instance != null && MinigameManager.instance.KizunaInMinigame()) || StateManager.instance.CurrPlayerState != ValidPlayerState.KizunaDemi;
    }
}
