using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCircleAttack : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
