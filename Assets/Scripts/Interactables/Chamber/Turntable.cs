using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : Interactable
{
    public GameEvent engageTurntable;
    
    void Update() {
        TryInteract();
    }

    public override void Interact() {
        Debug.Log("Player engaged table");
        engageTurntable.TriggerEvent();
    }
} 
