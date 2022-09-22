using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int capacity = 6;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("Inventory capacity excedded.");
            return;
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].nameOfItem == item.nameOfItem)
            {
                items[i].amount++;
                if (onItemChangedCallback != null)
                    onItemChangedCallback.Invoke();
                return;
            }
        }
        items.Add(item);

        // shows item obtained hint after each item is added
        ItemObtainedHint.instance.Show(item);

        if (onItemChangedCallback != null)
        onItemChangedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    public bool Contains(string itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].nameOfItem == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public bool Contains(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    public int AmountOf(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].nameOfItem == item.nameOfItem)
            {
                return items[i].amount;
            }
        }
        return 0;
    }

    public int Size()
    {
        return items.Count;
    }

    public void Decrease(Item item)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].nameOfItem == item.nameOfItem)
            {
                if (items[i].amount > 1)
                {
                    items[i].amount--;
                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke();
                    return;
                }
                else
                {
                    items.Remove(item);
                    if (onItemChangedCallback != null)
                        onItemChangedCallback.Invoke();
                    return;
                }
            }
        }
        Debug.Log("can't decrease because does not contain item");
        return;
    }

}
