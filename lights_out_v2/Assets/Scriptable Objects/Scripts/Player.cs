using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Player : ScriptableObject
{
    public float DEFAULT_currentHealth;
    public float DEFAULT_maxHealth;
    public float DEFAULT_souls;
    public int DEFAULT_numberOfHearts;
    public int DEFAULT_numberOfSouls;
    public int DEFAULT_numberOfKeys;
    public float[] DEFAULT_positions;
    public PlayerState DEFAULT_state;


    public float currentHealth;
    public float maxHealth;
    public float souls;
    public int numberOfHearts;
    public int numberOfSouls;
    public int numberOfKeys;
    public float[] positions;
    public PlayerState state;

}
