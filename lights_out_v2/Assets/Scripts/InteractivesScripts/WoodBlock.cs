using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WoodBlock : MonoBehaviour
{ 
    public BoolValue isOpen;
    public Animator animator;
    public Collider2D doorCollider;

    private void Start()
    {
        if (isOpen.value)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void LateUpdate()
    {
        animator.SetBool("isOpen", isOpen.value);
    }
    public void OpenDoor() 
    {
        isOpen.value = true;
        doorCollider.enabled = false;
       
    }

    public void CloseDoor()
    {
        isOpen.value = false;
        doorCollider.enabled = true;
    }
}
