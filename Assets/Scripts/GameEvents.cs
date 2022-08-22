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
}
