using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    key, 
    soul, 
    heart,
    common
}
[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public string itemDescription;
    public ItemType type;
}
