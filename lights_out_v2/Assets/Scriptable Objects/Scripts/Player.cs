using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    public float currentHealth;
    public float maxHealth;
    public float souls;
    public int numberOfHearts;
    public int numberOfSouls;
    public int numberOfKeys;
    public float[] positions;
    public PlayerState state;
}
