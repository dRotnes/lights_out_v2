using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactive
{
    private bool _canClose;
    private Collider2D triggerArea;
    private bool _isAppearing;

    public Inventory playerInventory;
    public Animator animator;
    public SignalSend raiseItem;
    public Item item;
    public SignalSend finishedAppearing;
    private bool _isOpen;

    private SavingManager _sm;
    private void Awake()
    {
        _sm = FindObjectOfType<SavingManager>();
        triggerArea = GetComponent<Collider2D>();
        _sm.AddToArray(this, null, null);
    }


    private void Update()
    {
        if (playerInRange)
        {
            switch (!_isOpen)
            {
                case true:
                    HandleInteractivesUI();
                    break;
                case false:
                    break;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!_isOpen)
                {
                    Debug.Log("cria");
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
        if (_isAppearing)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Lerp(0, 1, 3f * Time.deltaTime));
            if(spriteRenderer.color.a == 1) 
            {
                _isAppearing = false;
                finishedAppearing.RaiseSignal();
            }
        }
    }
    private void LateUpdate()
    {
        animator.SetBool("Open", _isOpen);
    }
    private void Open()
    {
        _isOpen = true;
        _canClose = true;
        playerInventory.currentItem = item;
        playerInventory.AddItem(playerInventory.currentItem);
        raiseItem.RaiseSignal();
        
    }
    private void chestIsOpen()
    {
        raiseItem.RaiseSignal();
        controllerSignals[1].RaiseSignal();
        pcSignals[1].RaiseSignal();
        _canClose = false;
    }

    public void SetOpen(bool open)
    {
        _isOpen = open;
    }

    public bool GetStatus()
    {
        return _isOpen;
    }
}
