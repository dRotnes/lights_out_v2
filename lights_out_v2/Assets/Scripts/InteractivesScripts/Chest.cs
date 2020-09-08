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
    public BoolValue isOpen;
    public SignalSend finishedAppearing;
    private void Start()
    {
        triggerArea = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            switch (!isOpen.value)
            {
                case true:
                    HandleInteractivesUI();
                    break;
                case false:
                    break;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!isOpen.value)
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
        animator.SetBool("Open", isOpen.value);
    }
    private void Open()
    {
        isOpen.value = true;
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
        interrogationSignalOff.RaiseSignal();
        _canClose = false;
    }

    public void SetOpen(bool open)
    {
        isOpen.value = open;
    }

    public bool GetOpen()
    {
        return isOpen.value;
    }

    public void Appear()
    {
        _isAppearing = true;
        
    }
    public void TriggerAppearence(GameObject anim)
    {
        anim.SetActive(true);
    }
}
