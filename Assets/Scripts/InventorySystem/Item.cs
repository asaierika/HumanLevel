using UnityEngine;

/**
* Non-perishable items e.g. map.
*/
[CreateAssetMenu(menuName = "Non-perishable item")]
public class Item : ScriptableObject
{
    public string nameOfItem;
    public Sprite icon;
    public int amount = 1;
    public Sprite itemImage;
    public string description;

    // Item effects
    public virtual bool Use() {
        return false;
    }
}
