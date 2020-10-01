using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    public bool controllerOn;
    void Update()
    {
        string[] array = Input.GetJoystickNames();
        if (array.Length > 0)
        {
            if (array[0].Length > 1)
            {
                controllerOn = true;
            }
            else
            {
                controllerOn = false;
            }

        }
        
    }
}
