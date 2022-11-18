using System.Collections.Generic;
using UnityEngine;

// #TODO: Change to concurrent implementation of dictionary and list for faster access.
public class Inventory : MonoBehaviour
{
    public delegate void OnItemChanged();
    public delegate void OnNewItemAdded(Item item);
    public delegate void OnItemUsed(Item item);
    public OnItemChanged onItemChangedCallback;
    public OnNewItemAdded onNewItemAddedCallback;
    public OnItemUsed onItemUsedCallback;


    public int capacity = 6;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("Inventory capacity exceeded.");
            return;
        }

        // Existing item.
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].nameOfItem == item.nameOfItem)
            {
                items[i].amount++;
                onItemChangedCallback?.Invoke();
                return;
            }
        }
        // New item.
        items.Add(item);

        // shows item obtained hint after each item is added
        onNewItemAddedCallback?.Invoke(item);
        onItemChangedCallback?.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback?.Invoke();
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

    public void UseItem(Item item)
    {

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].nameOfItem == item.nameOfItem)
            {
                bool noneLeft = items[i].Use();
                if (noneLeft) {
                    items.RemoveAt(i);
                }
                onItemChangedCallback?.Invoke();
                onItemUsedCallback?.Invoke(item);
                return;
            }
        }
        // Should not occur.
        Debug.Log("Inventory does not contain item.");
    }
}
