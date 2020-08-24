using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLighter : Interactive
{
    private bool _canDisactivate;

    public BoolValue isActive;
    public Animator animator;
    public SignalSend activated;

    //puzzle stuff
    /*public List<GameObject> activatingStuff = new List<GameObject>();
    public List<GameObject> deactivatingStuff = new List<GameObject>();*/

    public FireLighter[] fireLighterToDeactivate;
    private void Update()
    {
        if (playerInRange)
        {
            switch (!isActive.value)
            {
                case true:
                    HandleInteractivesUI();
                    break;
                case false:
                    break;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!isActive.value)
                {
                    Debug.Log("cria");
                    Activate();
                }
            }
        }
       
    }
    private void LateUpdate()
    {
        animator.SetBool("Active", isActive.value);
    }

    public void Activate()
    {
        isActive.value = true;
        activated.RaiseSignal();
        /*if (activatingStuff.Count > 0)
        {
            foreach(GameObject gO in activatingStuff)
            {
                gO.SetActive(true);
            }
        }
        if (deactivatingStuff.Count > 0)
        {
            foreach (GameObject gO in deactivatingStuff)
            {
                gO.SetActive(false);

            }
        }
        if (fireLighterToDeactivate.Length > 0)
        {
            foreach (FireLighter fl in fireLighterToDeactivate)
            {
                fl.Disactivate();

            }
        }*/
    }

    public void Disactivate()
    {
        isActive.value = false;
    }

    public bool GetStatus()
    {
        return isActive.value;
    }
}
