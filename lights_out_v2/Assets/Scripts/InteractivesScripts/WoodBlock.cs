using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WoodBlock : MonoBehaviour
{ 
    public bool isOpen;
    public Animator animator;
    public Collider2D doorCollider;

    private void Start()
    {
        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }
    public void OpenDoor() 
    {
        isOpen = true;
        doorCollider.enabled = false;
        animator.SetBool("isOpen", isOpen);
    }

    public void CloseDoor()
    {
        isOpen = false;
        doorCollider.enabled = true;
        animator.SetBool("isOpen", isOpen);
    }
}
