using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEye : BossEye
{
    public void StartAtk()
    {
        StartCoroutine(StartAtkCO());
    }


    private IEnumerator StartAtkCO()
    {
        //Instancia circulos de dano igual do inimigozinho na posicao que o player está!
        yield return null;
    }
}
