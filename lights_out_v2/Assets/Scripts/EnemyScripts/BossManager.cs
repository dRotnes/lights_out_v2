using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossManager : MonoBehaviour
{
    public List<BossEye> eyes = new List<BossEye>();
    public float health;
    public float startAttackTime;
    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;
    public SignalSend dead;

    private BossEye currentEye;
    private float _attackTime;
    private bool _canAttack;
    private bool _isDead;



    private void Start()
    {
        health = 50;
        _attackTime = startAttackTime;
        _canAttack = true;
        healthSlider.maxValue = health;
        UpdateHealth();
    }
    private int counter;

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthSlider.value = health;
        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }
    private void Update()
    {
        if (health <= 0 && !_isDead)
        {
            dead.RaiseSignal();
            Done();
            _isDead = true;
        }
        if (_canAttack && !_isDead)
        {
            if (_attackTime < 0)
            {
                foreach (BossEye eye in eyes)
                {
                    eye.PrepareToAttack();
                }
                _canAttack = false;
                _attackTime = startAttackTime;
            }
            else
            {
                _attackTime -= Time.deltaTime;
            }
        }

    }

    private IEnumerator Attack()
    {
        int index = Random.Range(0, 3);
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

    public void Ready()
    {
        _canAttack = true;
    }

    public void Done()
    {
        CinemachineShake.Instance.ShakeCam(1f, 2f);
    }
}