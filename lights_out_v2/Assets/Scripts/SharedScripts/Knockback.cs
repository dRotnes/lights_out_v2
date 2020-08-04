using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    public void Knock(Collider2D collision, Rigidbody2D thisRb)
    {
        thisRb.WakeUp();
        Vector2 difference = transform.position - collision.transform.position;
        difference = difference.normalized * thrust;
        thisRb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(RigidBodyWakeUP(thisRb));
        
    }

    private IEnumerator RigidBodyWakeUP(Rigidbody2D rb) {
        yield return new WaitForSeconds(knockTime);
        rb.Sleep();
    }
}
