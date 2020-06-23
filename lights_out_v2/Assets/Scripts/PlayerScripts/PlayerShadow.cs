using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public Transform fireball;
    public Transform floorPoint;

    private void Update()
    {
        Vector2 distance = floorPoint.position - fireball.position;

        if(distance.y < 0)
        {

        }
    }
}
