using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Interactive
{
    public SignalSend saveUISignalOn;
    public SignalSend saveUISignalOff;
    private SavingManager _savingManager;

    private void Start()
    {
        _savingManager = FindObjectOfType<SavingManager>();
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (interrogation)
                HandleInteractivesUI();

            if (Input.GetButtonDown("Fire2"))
            {
                Save();
            }
        }

    }
    private void Save()
    {
        _savingManager.SaveGame();
        saveUISignalOn.RaiseSignal();
    }

}
