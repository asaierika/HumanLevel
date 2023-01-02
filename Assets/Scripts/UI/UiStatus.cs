using System;

public class UiStatus
{
    public static event Action onOpenUI;
    public static event Action onCloseUI;

    public static void OpenUI()
    {
        if (onOpenUI != null) {
            onOpenUI();
        }
    }

    public static void CloseUI()
    {
        if (onCloseUI != null) {
            onCloseUI();
        }
    }
}
