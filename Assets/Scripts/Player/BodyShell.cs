using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyShell : Interactable
{
    public GameEvent switchToDemi;

    void Update() {
        TryInteract();
    }
    public override void Interact() {
        switchToDemi.TriggerEvent();
    }
}
