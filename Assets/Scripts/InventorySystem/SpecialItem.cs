using System;
using UnityEngine;

// Special Items can only be used when in the 
// range of specified Interactable Objects.
// When being used, it triggers the same effect 
// as if the player interact with the Interactable objects.
[CreateAssetMenu(menuName = "Special Item")]
public class SpecialItem : Item
{
    // The game event triggered when the item is used.
    // Since after the use of every special item the inventory
    // should be closed, there should be a SpecialItemGameEvent 
    // that extends GameEvent which should invoke 
    // onSpecialItemUsedCallback which disables the inventory UI.
    public SpecialItemGameEvent gameEvent;

    public override bool Use()
    {
        base.Use();
        
        gameEvent.TriggerEvent();

        return amount == 0;
    }
}
