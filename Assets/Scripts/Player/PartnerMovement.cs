using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartnerMovement : CharacterMovement
{
    public override void TryRestore() {
        characterFrozen = UiStatus.isOpen || (MinigameManager.instance != null && MinigameManager.instance.PartnerInMinigame()) || StateManager.instance.CurrPlayerState != ValidPlayerState.PartnerDemi;
    }
}
