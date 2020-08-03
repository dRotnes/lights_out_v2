using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public int numberOfKeys;
    public int runtimeNumberOfKeys;

    public void OnBeforeSerialize()
    {
        runtimeNumberOfKeys = numberOfKeys;
    }

    public void AddItem(Item item)
    {
        if (item.isKey)
        {
            numberOfKeys++;
        }
        else
        {
            if (!items.Contains(item))
            {
                items.Add(item);
            }
        }
    }

    public void OnAfterDeserialize()
    {
        numberOfKeys = 0;
    }
}

