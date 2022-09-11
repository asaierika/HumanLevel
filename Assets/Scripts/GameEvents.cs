using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;

    private void Awake()
    {
        instance = this;
    }

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

    public event Action onEnterSpiritMode;

    public void EnterSpiritMode()
    {
        if (onEnterSpiritMode != null) 
        {
            EnterSpiritMode();
        }
    }

    public event Action onExitSpiritMode;

    public void ExitSpiritMode()
    {
        if (onExitSpiritMode != null) 
        {
            ExitSpiritMode();
        }
    }
}
