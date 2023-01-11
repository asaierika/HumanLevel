public class SpiritMovement : CharacterMovement {
    public override void TryRestore() {
        // Reevaluate whether movement should be frozen.
        characterFrozen = UiStatus.isOpen || (MinigameManager.instance != null && MinigameManager.instance.KizunaInMinigame()) || StateManager.instance.CurrPlayerState != ValidPlayerState.KizunaSpirit;
    }
}
