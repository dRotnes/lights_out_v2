using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactive
{
    private bool _canClose = true;
    private bool _isOpen;
    private Collider2D triggerArea;

    public Inventory playerInventory;
    public Animator animator;
    public SignalSend raiseItem;
    public Item item;
    private void Start()
    {
        triggerArea = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            if(interrogation)
                HandleInteractivesUI();
            if (Input.GetButtonDown("Fire2")) 
            {
                if (!_isOpen)
                {

                    Open();
                }
                else
                {
                    if (_canClose)
                        chestIsOpen();
                    else
                        return;
                }
            }
        }
    }
    private void Open()
    {
        animator.SetTrigger("Open");
        playerInventory.currentItem = item;
        playerInventory.AddItem(playerInventory.currentItem);
        raiseItem.RaiseSignal();
        _isOpen = true;
        _canClose = true;
        
    }
    private void chestIsOpen()
    {
        raiseItem.RaiseSignal();
        controllerSignals[1].RaiseSignal();
        pcSignals[1].RaiseSignal();
        interrogationSignalOff.RaiseSignal();
        interrogation = false;
        _canClose = false;
    }
}
