using UnityEngine;

public class Sign : Interactive
{
    public DialogTrigger dialog;
    private bool _canDesactivate;

    private void Update()
    {
        if (playerInRange)
        {
            _canDesactivate = true;

            if(Input.GetKeyDown("space") && dialog.ReturnState() == DialogState.unactive)
            {
                dialog.TriggerDialog();
            }
            

        }
        else
        {
            if (dialog.ReturnState() != DialogState.unactive && _canDesactivate)
            {
                dialog.EndDialog();
            }
            _canDesactivate = false;
        }
    }
  
}
