using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactive
{
    private bool _canOpen = true;
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
        if (playerInRange && Input.GetKeyDown("space"))
        {
            if (!_isOpen)
            {

                Open();
            }
            else
            {
                if (_canOpen == false)
                    chestIsOpen();
                else
                    return;
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
        _canOpen = false;
        
    }
    private void chestIsOpen()
    {
        raiseItem.RaiseSignal();
        signalOff.RaiseSignal();
        interrogation = false;
    }
}
