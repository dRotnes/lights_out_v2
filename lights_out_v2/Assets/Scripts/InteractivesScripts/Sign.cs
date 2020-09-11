using UnityEngine;

public class Sign : Interactive
{
    public DialogTrigger dialogTrigger;
    private bool _canDesactivate;

    private void Update()
    {
        if (playerInRange)
        {
            if (canInteract)
            {

                HandleInteractivesUI();
            }
            _canDesactivate = true;

            if (Input.GetButtonDown("Fire2"))
            {
                if (dialogTrigger.ReturnState() == DialogState.unactive)
                {
                    dialogTrigger.TriggerDialog();
                }
                else if(dialogTrigger.ReturnState() == DialogState.active && dialogTrigger.dialog.isTyped != true)
                {
                    dialogTrigger.DisplayNextSentence();
                }
            }
        }
        else
        {
            if (dialogTrigger.ReturnState() == DialogState.active && _canDesactivate)
            {
                dialogTrigger.EndDialog();
            }
            _canDesactivate = false;
        }
    }
}
