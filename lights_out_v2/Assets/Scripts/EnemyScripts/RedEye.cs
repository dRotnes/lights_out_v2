using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEye : BossEye
{
    public float startTimeBtwShots;
    public float force;
    public GameObject bullet;
    public Transform shotPoint;
    public Transform playerPos;

    private float _timeBtwShots;
    private bool canShoot;
    public void StartAtk()
    {
        Debug.Log(this.name + " monkeydonkey");
        state = EyeState.attacking;
        animator.SetTrigger("Attack");
        _timeBtwShots = startTimeBtwShots;
        canShoot = true;
        StartCoroutine(Shoot());
    }

    private void Update()
    {
        if (canShoot)
        {
            if(_timeBtwShots <= 0)
            {
                StopAllCoroutines();
                canShoot = false;
                Fall();
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
                
            }
        }

        shotPoint.LookAt(playerPos);
    }


    private IEnumerator Shoot()
    {
        
        GameObject ob = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        Rigidbody2D rb = ob.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.forward * force, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Shoot());
    }

}
