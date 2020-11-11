using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public SignalSend finishSignal;

    private int numberOfActive = 0;

    public void AddOne()
    {
        numberOfActive += 1;
        Debug.Log(numberOfActive);
        
        if(numberOfActive == 3)
        {
            finishSignal.RaiseSignal();
        }
    }
}
