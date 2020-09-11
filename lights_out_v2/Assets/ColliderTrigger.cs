using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    public SignalSend[] signals;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach(SignalSend s in signals)
            {
                s.RaiseSignal();
            }
        }
    }
}
