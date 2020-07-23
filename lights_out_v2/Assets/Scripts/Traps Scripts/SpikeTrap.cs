using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    private bool canActivate = true;
    private bool isActive;
    private float _damageRate = 0;

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Active"))
        {
            if (_damageRate <= 0)
            {
                Damage();
                _damageRate = startDamageRate;
            }
            else
            {
                _damageRate -= Time.deltaTime;
            }
        }

        switch (playerInRange) {
            case true:
                StopCoroutine("Desactivate");
                switch (canActivate) {
                    case true:
                        isActive = true;
                        canActivate = false;
                        break;
                    case false:
                        return;
                }
                break;

            case false:
                _damageRate = 0;
                switch (isActive)
                {
                    case true:
                        StartCoroutine("Desactivate");
                        break;
                    case false:
                        return;
                }
                break;
        }

        
    }

    private void LateUpdate()
    {
        animator.SetBool("IsActive", isActive);
    }
    private void Damage()
    {
        if (playerCollider!=null)
        {
            playerCollider.GetComponentInParent<PlayerHealth>().TakeDamage(trapDamage);
        }
        else
        {
            Debug.Log("Yessir");
        }
    }

    private IEnumerator Desactivate()
    {
        yield return new WaitForSeconds(timeActive);
        isActive = false;
        StartCoroutine("WaitToActivateAgain");
    }
    private IEnumerator WaitToActivateAgain()
    {
        yield return new WaitForSeconds(timeUnactive);
        canActivate = true;

    }
}
