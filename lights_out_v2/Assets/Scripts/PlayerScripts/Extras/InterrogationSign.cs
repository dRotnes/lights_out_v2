using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterrogationSign : MonoBehaviour
{
    public GameObject interrogationSign;
    

    public void Enable()
    {
       
        interrogationSign.SetActive(true);
      
        
    }

    public void Disable()
    {
        interrogationSign.SetActive(false);
       
    }
}
