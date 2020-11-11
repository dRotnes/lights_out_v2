using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    public SignalSend finishSignal;

    private int numberOfActive;

    public void AddOne()
    {
        numberOfActive += 1;
        
        if(numberOfActive == 3)
        {
            finishSignal.RaiseSignal();
        }
    }
}
