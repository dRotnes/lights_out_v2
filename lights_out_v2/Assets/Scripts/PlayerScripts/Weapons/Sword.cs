using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackDamage;
    private void OnCollisionEnter2D()
    {
        Debug.Log("Attacking correctly " + attackDamage.ToString());
    }

}
