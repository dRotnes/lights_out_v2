using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WoodBlock : MonoBehaviour
{ 
    private bool _isOpen;
    public Animator animator;
    public Collider2D doorCollider;   
    public void OpenDoor() {

        _isOpen = true;
        doorCollider.enabled = false;
        animator.SetBool("isOpen", _isOpen);
    }

    public void CloseDoor()
    {
        _isOpen = false;
        doorCollider.enabled = true;
        animator.SetBool("isOpen", _isOpen);
    }

}
