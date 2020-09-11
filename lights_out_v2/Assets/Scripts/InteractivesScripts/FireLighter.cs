using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLighter : Interactive
{
    private bool _canDisactivate;
    private bool _isActive;

    public Animator animator;
    public SignalSend activated;

    //puzzle stuff
    /*public List<GameObject> activatingStuff = new List<GameObject>();
    public List<GameObject> deactivatingStuff = new List<GameObject>();*/

    public FireLighter[] fireLighterToDeactivate;

    [SerializeField] private SavingManager _sm;
    private void Awake()
    {
        _sm = FindObjectOfType<SavingManager>();
        _sm.AddToArray(null, this, null);
    }
    private void Update()
    {
        if (playerInRange)
        {
            switch (!_isActive)
            {
                case true:
                    HandleInteractivesUI();
                    break;
                case false:
                    break;
            }
            if (Input.GetButtonDown("Fire2"))
            {
                if (!_isActive)
                {
                    Debug.Log("cria");
                    Activate();
                }
            }
        }
       
    }
    private void LateUpdate()
    {
        animator.SetBool("Active", _isActive);
    }

    public void Activate()
    {
        _isActive= true;
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
        _isActive = false;
    }

    public bool GetStatus()
    {
        return _isActive;
    }
    public void SetStatus(bool status)
    {
        _isActive = status;
    }
}
