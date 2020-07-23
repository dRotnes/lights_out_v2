using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Trap Generals")]
    public Animator animator;
    public float timeActive;
    public float timeUnactive;
    public float trapDamage;
    public float startDamageRate;
    public bool playerInRange;
    public Collider2D playerCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FootCollider"))
        {
            playerInRange = true;
            playerCollider = collision;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FootCollider"))
        {
            playerInRange = false;
            playerCollider = null;
        }
    }
}
