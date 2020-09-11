using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WoodBlock : MonoBehaviour
{ 
    private bool _isOpen;
    public Animator animator;
    public Collider2D doorCollider;

    [SerializeField] private SavingManager _sm;
    
    private void Awake()
    {
        _sm = FindObjectOfType<SavingManager>();
        _sm.AddToArray(null, null,this);
        
    }
    private void Start()
    {
        if (_isOpen)
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
        animator.SetBool("isOpen", _isOpen);
        doorCollider.enabled = !_isOpen;
    }
    public void OpenDoor() 
    {
        _isOpen = true;
    }

    public void CloseDoor()
    {
        _isOpen = false;
    }
    public bool GetStatus()
    {
        return _isOpen;
    }
    public void SetStatus(bool status)
    {
        _isOpen = status;
    }
}
