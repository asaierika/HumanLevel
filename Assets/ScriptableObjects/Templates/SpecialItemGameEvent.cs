using System.Collections.Generic;
using UnityEngine;

// Since after the use of every special item the inventory
// should be closed, there should be a SpecialItemGameEvent 
// that extends GameEvent which should invoke 
// onSpecialItemUsedCallback which disables the inventory UI.
[CreateAssetMenu(menuName="Special Item Game Event")]
public class SpecialItemGameEvent : GameEvent
{
    public override void TriggerEvent() {
        // Event triggered should call the use item function in the respective Interactable object
        base.TriggerEvent();
        GameManager.instance.inventory.onSpecialItemUsedCallback?.Invoke();
    }
}
