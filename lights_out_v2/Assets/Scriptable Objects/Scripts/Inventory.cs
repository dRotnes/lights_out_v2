using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public SignalSend heartContainerSignal;
    public int numberOfKeys;
    public int numberOfSouls;
    public int runtimeNumberOfSouls;
    public int runtimeNumberOfKeys;

    public void OnBeforeSerialize()
    {
        runtimeNumberOfKeys = numberOfKeys;
        runtimeNumberOfSouls = numberOfSouls;
    }

    public void AddItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.key:
                numberOfKeys++;
                break;
            case ItemType.soul:
                numberOfSouls++;
                break;
            case ItemType.heart:
                heartContainerSignal.RaiseSignal();
                break;
            case ItemType.common:
                if (!items.Contains(item))
                {
                    items.Add(item);
                }
                break;

        }
        
       
        
    }

    public void OnAfterDeserialize()
    {
        numberOfKeys = 0;
        numberOfSouls = 0;
    }
}

