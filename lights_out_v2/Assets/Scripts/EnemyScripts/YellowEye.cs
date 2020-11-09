using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEye : BossEye
{
    public float startAttackTime;
    public GameObject circle;

    private float _attackTime;
    private bool canAttack;
    public void StartAtk()
    {
        Debug.Log(this.name + " monkeydonkey");
        state = EyeState.attacking;
        animator.SetTrigger("Attack");
        _attackTime = startAttackTime;
        canAttack = true;
        StartCoroutine(Attack());
    }

    private void Update()
    {
        if (canAttack)
        {
            if (_attackTime <= 0)
            {
                StopAllCoroutines();
                canAttack = false;
                Fall();
            }
            else
            {
                _attackTime -= Time.deltaTime;

            }
        }
    }


    private IEnumerator Attack()
    {
        Instantiate(circle, playerPos.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        StartCoroutine(Attack());
    }
}
