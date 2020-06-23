using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Attacking correctly the " + collision.name);
    }

}
