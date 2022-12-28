using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string nameOfItem;
    public Sprite icon;
    public int amount = 1;
    public Sprite itemImage;
    public string description;

    // The behaviour of the item when the player chooses
    // the item in the inventory.
    public abstract bool Use();
}
