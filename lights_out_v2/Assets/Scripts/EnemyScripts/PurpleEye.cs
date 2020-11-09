using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEye : BossEye
{
    public float startAttackTime;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public Transform finalPoint;
    public GameObject globalLight;
    public GameObject effectStart;
    public GameObject effectEnd;

    private float _attackTime;
    private bool _canShoot;
    private void Awake()
    {
        DisableLaser();
    }
    public void StartAtk()
    {
        state = EyeState.attacking;
        animator.SetTrigger("Attack");
        _attackTime = startAttackTime;
        StartCoroutine(StartAtkCO());
        _canShoot = true;
    }

    private void Update()
    {
        if (_canShoot)
        {
            if (_attackTime <= 0)
            {
                _canShoot = false;
                DisableLaser();
                Fall();
                
            }
            else
            {
                UpdateLaser();
                _attackTime -= Time.deltaTime;

            }
        }
    }



    private IEnumerator StartAtkCO()
    {
        yield return new WaitForSeconds(0.5f);
        EnableLaser();
    }

    private void EnableLaser()
    {
        lineRenderer.enabled = true;
        globalLight.SetActive(true);
        effectEnd.SetActive(true);
        GameObject effect = Instantiate(effectStart, firePoint.position, Quaternion.identity);
        Destroy(effect, 9.6f);
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, finalPoint.position);

        Vector2 direction = (Vector2)finalPoint.position - (Vector2)transform.position;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction.normalized, direction.magnitude);

        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }
        effectEnd.transform.position = lineRenderer.GetPosition(1);

    }

    private void DisableLaser()
    {
        lineRenderer.enabled = false;
        globalLight.SetActive(false);
        effectEnd.SetActive(false);
    }
}

