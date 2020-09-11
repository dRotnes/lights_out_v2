using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key, 
    common
}
public class Door : Interactive
{

    public DoorType doortype;
    public Player player;
    public bool canOpen = true;

    public Animator animator;
    public Collider2D mainCollider;

    private bool _isOpen;

    private void Update()
    {
        if (playerInRange)
        {
            switch (!_isOpen)
            {
                case true:
                    if(canOpen)
                        HandleInteractivesUI();
                    break;
                case false:
                    break;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!_isOpen && canOpen)
                {
                    OpenDoor();
                }
            }
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("Open", _isOpen);
        mainCollider.enabled = !_isOpen;
        
    }

    public void OpenDoor()
    {
        switch (doortype)
        {
            case DoorType.key:
                if (player.numberOfKeys > 0)
                    _isOpen = true;
                    player.numberOfKeys--;
                break;

            case DoorType.common:
                _isOpen = true;
                break;

        }
        canOpen = false;
    }
    public void CloseDoor()
    {
        _isOpen = false;
    }
}
