using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEye : BossEye
{
    public float startAttackTime;
    public float force;
    public GameObject bullet;
    public Transform shotPoint;


    private float _attackTime;
    private bool canShoot;
    public void StartAtk()
    {
        Debug.Log(this.name + " monkeydonkey");
        state = EyeState.attacking;
        animator.SetTrigger("Attack");
        _attackTime = startAttackTime;
        canShoot = true;
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if (canShoot)
        {
            if (_attackTime <= 0)
            {
                StopAllCoroutines();
                canShoot = false;
                Fall();
            }
            else
            {
                _attackTime -= Time.deltaTime;

            }
        }

        shotPoint.LookAt(playerPos);
    }


    private IEnumerator Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Shot");
        GameObject ob = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        Rigidbody2D rb = ob.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.forward * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.5f);
        StartCoroutine(Shoot());
    }

}