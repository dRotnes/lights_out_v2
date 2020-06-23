using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform _fireballPosition;
    public float baseDistance;
    public float speed;
    private void Awake()
    {
        
        _fireballPosition = GameObject.Find("FireballPosition").transform;
    }
    void Start()
    {
        transform.position = _fireballPosition.position;
    }
    
    void Update()
    {
        float distance = Vector2.Distance(transform.position, _fireballPosition.position);
        
        if(distance > baseDistance)
        {
        
            transform.position = Vector2.MoveTowards(transform.position, _fireballPosition.position, speed * Time.deltaTime);
        }
        
    }
}
