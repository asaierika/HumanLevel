using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Consumable Item")]
public class ConsumableItem : Item
{
    public override bool Use()
    {
        amount = Math.Max(0, amount - 1);
        return amount == 0;
    }
}
