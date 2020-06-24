using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject globalLight;
    public float intensity;

    void Update()
    {
        

        if(globalLight.activeSelf == true)
        {
            gameObject.GetComponentInChildren<Light2D>().intensity = 0;
        }
        else
        {
            gameObject.GetComponentInChildren<Light2D>().intensity = intensity;
        }
    }
}
