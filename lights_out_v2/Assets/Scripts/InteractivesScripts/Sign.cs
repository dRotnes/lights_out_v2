using UnityEngine;

public class Sign : Interactive
{
    public DialogTrigger dialog;
    public bool canDesactivate;

    private void Update()
    {
        if (playerInRange)
        {
            canDesactivate = true;
            if(Input.GetKeyDown("space") && dialog.ReturnState() == DialogState.unactive)
            {
                dialog.TriggerDialog();
            }

        }
        else
        {
            if (dialog.ReturnState() != DialogState.unactive && canDesactivate)
            {
                dialog.EndDialog();
            }
            canDesactivate = false;
        }
    }
  
}
