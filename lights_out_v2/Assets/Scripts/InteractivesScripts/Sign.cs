using UnityEngine;

public class Sign : Interactive
{
    public DialogTrigger dialogTrigger;
    private bool _canDesactivate;

    private void Update()
    {
        if (playerInRange)
        {
            _canDesactivate = true;

            if (Input.GetKeyDown("space"))
            {
                if (dialogTrigger.ReturnState() == DialogState.unactive)
                {
                    dialogTrigger.TriggerDialog();
                }
                else if(dialogTrigger.ReturnState() == DialogState.active)
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
