using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool playerInRange;
    public bool isFixed;
    public SpriteRenderer spriteRenderer;
    public SignalSend signalOn;
    public SignalSend signalOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signalOn.RaiseSignal();
            playerInRange = true;
            if (!isFixed)
            {
                spriteRenderer.sortingLayerName = "MiddleGround";
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signalOff.RaiseSignal();
            playerInRange = false;
            if (!isFixed)
            {
                spriteRenderer.sortingLayerName = "ForeGround";
            }
            
        }
    }
}
