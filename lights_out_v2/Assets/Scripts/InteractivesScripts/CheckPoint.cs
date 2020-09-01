using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CheckPoint : Interactive
{
    public SignalSend saveUISignalOn;
    public SignalSend saveUISignalOff;
    private SavingManager _savingManager;
    public Transform checkPointPosition;
    public Player playerStats;

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
        playerStats.positions = new float[] { checkPointPosition.position.x, checkPointPosition.position.y, checkPointPosition.position.z};
        _savingManager.SaveGame(SceneManager.GetActiveScene().buildIndex);
        saveUISignalOn.RaiseSignal();
    }

}
