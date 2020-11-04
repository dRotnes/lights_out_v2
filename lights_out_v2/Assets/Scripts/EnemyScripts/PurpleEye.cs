using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEye : BossEye
{
    public void StartAtk()
    {
        StartCoroutine(StartAtkCO());
    }


    private IEnumerator StartAtkCO()
    {
        Debug.Log(this.name + " monkeydonkey");
        state = EyeState.attacking;
        animator.SetTrigger("Attack");
        //Instancia circulos de dano igual do inimigozinho na posicao que o player está!
        yield return null;
    }
}
