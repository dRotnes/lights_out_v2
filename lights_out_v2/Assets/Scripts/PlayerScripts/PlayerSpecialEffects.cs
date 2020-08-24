using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialEffects : MonoBehaviour
{
    public GameObject specialAtkEffect;

    public void InstantiateSpecialAtk()
    {
        Instantiate(specialAtkEffect, transform.position, Quaternion.identity);

        CinemachineShake.Instance.ShakeCam(2f, .1f);
    }
}
