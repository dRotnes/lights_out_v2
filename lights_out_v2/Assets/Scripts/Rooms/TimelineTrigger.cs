using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour
{
    private PlayableDirector _playable;


    private void Awake()
    {
        _playable = GetComponent<PlayableDirector>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            _playable.Play();
    }

    public void DestroySelf() {
        Destroy(this.gameObject);
    }
}
