﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLighter : Interactive
{
    private bool _canDisactivate;
    private bool _isActive;

    public Animator animator;
    public SignalSend activated;
    public bool canActivate;


    public FireLighter[] fireLighterToDeactivate;

    [SerializeField] private SavingManager _sm;
    private void Awake()
    {
        _sm = FindObjectOfType<SavingManager>();
        _sm.AddToArray(null, this, null);
    }
    private void Update()
    {
        if (playerInRange)
        {
            switch (_isActive)
            {
                case false:
                    HandleInteractivesUI();
                    break;
                case true:
                    break;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!_isActive)
                {
                    Activate();
                }
            }
        }

        if(_isActive && canActivate)
        {
            activated.RaiseSignal();
            Debug.Log("UDJAJS");
            canActivate = false;
        }
       
    }
    private void LateUpdate()
    {
        animator.SetBool("Active", _isActive);
    }

    public void Activate()
    {
        _isActive= true;
        activated.RaiseSignal();
    }

    public void Disactivate()
    {
        _isActive = false;
    }

    public bool GetStatus()
    {
        return _isActive;
    }
    public void SetStatus(bool status)
    {
        if(status == true)
        {
            _isActive = true;
            canActivate = true;
        }
    }
}
