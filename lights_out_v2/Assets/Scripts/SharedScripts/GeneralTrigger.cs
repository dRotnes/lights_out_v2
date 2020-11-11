using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GeneralTrigger : MonoBehaviour
{
    public bool playerInRange;
    public bool status;
    public SavingManager sm;
    private PlayableDirector _playable;
    private void Start()
    {
        _playable = GetComponent<PlayableDirector>();
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
    private void Update()
    {
        if (playerInRange && !status)
        {
            StartTimeline();
        }
    }

    public void StartTimeline()
    {
        _playable.Play();
        status = true;
    }
}
