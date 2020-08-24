using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<FireLighter> fireLighterList = new List<FireLighter>();
    
    /*public List<GameObject> activatingStuff = new List<GameObject>();
    public List<GameObject> deactivatingStuff = new List<GameObject>();*/

    public SignalSend finishSignal;

    private int numberOfActive;

    public void AddOne()
    {
        numberOfActive = 0;
        foreach(FireLighter fl in fireLighterList)
        {
            if (fl.GetStatus())
            {
                numberOfActive += 1;
            }
        }
        if(numberOfActive == fireLighterList.Count)
        {
            finishSignal.RaiseSignal();
        }
    }
}
