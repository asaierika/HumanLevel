using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string nameOfItem;
    public Sprite icon;
    public int amount = 1;
    public Sprite itemImage;
    public string discription;

    public virtual void Use()
    {
        ZoomInBox.instance.Show(this);
    }
}
