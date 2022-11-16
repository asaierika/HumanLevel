using UnityEngine;
using System;

public class UiStatus : MonoBehaviour
{
    public event Action onOpenUI;

    public void OpenUI()
    {
        if (onOpenUI != null)
        {
            onOpenUI();
        }
    }

    public event Action onCloseUI;

    public void CloseUI()
    {
        if (onCloseUI != null)
        {
            onCloseUI();
        }
    }
}
