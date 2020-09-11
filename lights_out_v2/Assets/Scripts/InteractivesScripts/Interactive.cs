using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    public bool playerInRange;
    public bool isFixed;
    public bool canInteract = true;
    public SpriteRenderer spriteRenderer;
    public SignalSend[] controllerSignals;
    public SignalSend[] pcSignals;
    public ControllerManager controllerManager;
    private void Awake()
    {
        controllerManager = FindObjectOfType<ControllerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            if (!isFixed)
            {
                if(collision.transform.position.y - transform.position.y > 0)
                    spriteRenderer.sortingLayerName = "ForeGround";
                else
                    spriteRenderer.sortingLayerName = "MiddleGround";
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controllerSignals[1].RaiseSignal();
            pcSignals[1].RaiseSignal();
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

    public void SetCanInteract(bool value)
    {
        canInteract = value;

    }

   
}
