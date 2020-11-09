using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPoint : MonoBehaviour
{
    private float _angle;

    public float speed;
    public float radius;

    private Vector2 _center;

    private void Start()
    {
        _center = transform.position;
    }

    private void Update()
    {
        _angle += Time.deltaTime * speed;

        float x = Mathf.Cos(_angle);
        float y = Mathf.Sin(_angle);

        var offset = new Vector2(x, y) * radius;

        transform.position = _center + offset; 

    }
}
