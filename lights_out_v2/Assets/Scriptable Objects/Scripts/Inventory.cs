using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item currentItem;
    public List<Item> items = new List<Item>();
    public SignalSend heartContainerSignal;
    public Player playerStats;

    public void AddItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.key:
                playerStats.numberOfKeys++;
                break;
            case ItemType.soul:
                playerStats.numberOfSouls++;
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
}

