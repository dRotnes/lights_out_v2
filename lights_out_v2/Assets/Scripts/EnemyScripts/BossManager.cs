using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    alive,
    dead
}
public class BossManager : MonoBehaviour
{
    public List<BossEye> eyes = new List<BossEye>();

    private BossEye currentEye;

    private float health;
    private int counter;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            foreach (BossEye eye in eyes)
            {
                eye.PrepareToAttack();
            }
        }

    }

    private IEnumerator Attack()
    {
        int index = Random.Range(0, 3);
        index = 0;
        currentEye = eyes[index];
        yield return new WaitForSeconds(2f);
        currentEye.StartAttacking();

    }

    public void FinishedAttacking()
    {
        StartCoroutine(FinishedAttackingCO());
    }

    private IEnumerator FinishedAttackingCO()
    {
        yield return new WaitForSeconds(1.5f);
        foreach (BossEye eye in eyes)
        {
            if (eye != currentEye)
                eye.Idle();
        }
    }

    public void CanAttack()
    {
        counter += 1;
        if (counter == eyes.Count)
        {
            StartCoroutine(Attack());
            counter = 0;
        }
    }
}