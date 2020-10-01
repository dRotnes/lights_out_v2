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

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            foreach(BossEye eye in eyes)
            {
                eye.PrepareToAttack();
            }
        }
    }

    public void Attack()
    {
        int index = Random.Range(0, 3);
        currentEye = eyes[index];
        currentEye.StartAttacking();
    }

    public void FinishedAttacking()
    {
        foreach (BossEye eye in eyes)
        {
            if(eye!=currentEye)
                eye.Idle();
        }
    }
}
