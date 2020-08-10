using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public bool controllerOn;
    void Update()
    {
        if (Input.GetJoystickNames()[1].Length > 0)
        {
            controllerOn = true;
        }
        else
        {
            controllerOn = false;
        }
    }
}
