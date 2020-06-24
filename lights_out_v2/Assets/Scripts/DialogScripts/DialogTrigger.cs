using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogTrigger : MonoBehaviour
{
    private DialogManager _dialogManager;
    public Dialog dialog;
    public bool isTyped;
    public bool playOnAwake;

    private void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
        if (playOnAwake)
        {
            TriggerDialog();
        }
    }
    public void TriggerDialog()
    {
        _dialogManager.StartDialog(dialog, isTyped);
    }

    public void EndDialog()
    {
        _dialogManager.EndDialog();
    }

    public DialogState ReturnState()
    {
        DialogState currentState = _dialogManager.currentState;
        return currentState;
    }

}
