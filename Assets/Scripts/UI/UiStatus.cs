using System;

public class UiStatus
{
    public static bool isOpen;
    public static event Action onOpenUI;
    public static event Action onCloseUI;

    public static void OpenUI()
    {
        CharacterMovement.playerFrozen = true;
        if (onOpenUI != null) {
            onOpenUI();
        }
    }

    public static void CloseUI()
    {
        CharacterMovement.playerFrozen = false;
        if (onCloseUI != null) {
            onCloseUI();
        }
    }
}
