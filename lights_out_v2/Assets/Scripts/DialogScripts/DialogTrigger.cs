using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    private DialogManager _dialogManager;
    public Dialog dialog;

    private void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
    }
    public void TriggerDialog(bool delay = false)
    {
        if (delay)
        {
            StartCoroutine(DelayedTrigger());
        }
        else
        {
            _dialogManager.StartDialog(dialog);

        }
    }

    private IEnumerator DelayedTrigger()
    {
        yield return new WaitForSeconds(2f);
        _dialogManager.StartDialog(dialog);
    }

    public void DisplayNextSentence()
    {
        if (_dialogManager.canpass)
        {
            _dialogManager.DisplayNextSentence();
        }
        else 
        {
            return;
        }
    }

    public void EndDialog()
    {
        _dialogManager.EndDialog();
    }

    public DialogState ReturnState()
    {
        return _dialogManager.currentState;
    }

}
