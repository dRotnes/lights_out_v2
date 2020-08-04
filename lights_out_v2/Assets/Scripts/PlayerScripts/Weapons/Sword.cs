using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float attackDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("damaged");
            collision.GetComponent<Enemy>().TakeDamage(attackDamage, GetComponent<Collider2D>());

        }
    }

}
