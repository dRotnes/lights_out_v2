using UnityEngine;

public class Sign : Interactive
{
    public DialogTrigger dialog;
    private bool canDesactivate;

    private void Update()
    {
        if (playerInRange)
        {
            canDesactivate = true;
            if(Input.GetKeyDown("space"))
            {
                if (dialog.ReturnState() == DialogState.unactive)
                {
                    Debug.Log("hello");

                    dialog.TriggerDialog();

                }
                else if(dialog.ReturnState() == DialogState.finished)
                {
                    dialog.EndDialog();
                }
            }

        }
        else
        {
            if (dialog.ReturnState() == DialogState.active || dialog.ReturnState() == DialogState.finished && canDesactivate)
            {
                dialog.EndDialog();
                canDesactivate = false;
            }
        }
    }
  
}
