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
    }
    private void Update()
    {
        if (playerInRange && !status)
        {
            _playable.Play();
            status = true;
        }
    }
}

