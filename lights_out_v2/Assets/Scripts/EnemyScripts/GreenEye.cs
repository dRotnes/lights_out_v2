using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEye : BossEye
{
    public float startAttackTime;
    public GameObject circle;
    public Transform centerPoint;

    private float _attackTime;
    private bool canShoot;


    public void StartAtk()
    {
        StartCoroutine(StartAtkCO());
    }

    private IEnumerator StartAtkCO()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(.4f);
        FindObjectOfType<AudioManager>().Play("CircleGreen");
        GameObject c = Instantiate(circle, centerPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(startAttackTime);
        Destroy(c);
        FindObjectOfType<AudioManager>().Stop("CircleGreen");
        Fall();
    }
}
