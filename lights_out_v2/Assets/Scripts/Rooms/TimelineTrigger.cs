using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : GeneralTrigger
{
    private PlayableDirector _playable;
    private void Start()
    {
        _playable = GetComponent<PlayableDirector>();
        if (status)
        {
            Destroy(gameObject);
        }
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

