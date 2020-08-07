using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool playerInRange;
    public bool isFixed;
    public bool interrogation = true;
    public SpriteRenderer spriteRenderer;
    public SignalSend interrogationSignalOn;
    public SignalSend interrogationSignalOff;
    public SignalSend[] controllerSignals;
    public SignalSend[] pcSignals;
    public ControllerManager controllerManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interrogation)
                interrogationSignalOn.RaiseSignal();
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
            if (interrogation)
                controllerSignals[1].RaiseSignal();
                pcSignals[1].RaiseSignal();
                interrogationSignalOff.RaiseSignal();
            playerInRange = false;
            if (!isFixed)
                spriteRenderer.sortingLayerName = "ForeGround";
        }
    }

    public void HandleInteractivesUI()
    {
        if (controllerManager.controllerOn)
        {
            controllerSignals[0].RaiseSignal();
            pcSignals[1].RaiseSignal();
        }
        else
        {
            pcSignals[0].RaiseSignal();
            controllerSignals[1].RaiseSignal();
        }
    }

   
}
