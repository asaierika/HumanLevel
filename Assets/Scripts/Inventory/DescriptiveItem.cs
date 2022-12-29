using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Descriptive Item")]
public class DescriptiveItem : Item
{
    // Descriptive Items can only be viewed and not consumed
    // The amount of the Item stays the same after the use.
    public override bool Use()
    {
        base.Use();
        
        GameManager.instance.inventory.onItemZoomedInCallback?.Invoke(this);
        return false;
    }
}
