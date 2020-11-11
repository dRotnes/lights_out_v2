using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTrigger : MonoBehaviour
{
    public bool playerInRange;
    public bool status;
    public SavingManager sm;

    private void Awake()
    {
        sm.AddToArray(null, null, null, null, this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInRange = false;
    }

    public void SetStatus(bool value)
    {
        status = value;
    }
    public bool GetStatus()
    {
        return status;
    }
}
