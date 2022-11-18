using System;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string nameOfItem;
    public Sprite icon;
    public int amount = 1;
    public Sprite itemImage;
    public string description;

    // Item effects
    public virtual bool Use() {
        amount = Math.Max(0, amount - 1);
        return amount == 0;
    }
}
