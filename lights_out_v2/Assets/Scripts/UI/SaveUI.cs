using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveUI : MonoBehaviour
{
    public TextMeshProUGUI tm;
    private bool _fading;
    private float alpha;

    private void Update()
    {
        if (_fading)
        {
            if (tm.color.a == 0)
            {
                _fading = false;
                tm.gameObject.SetActive(false);
            }
            alpha = Mathf.Lerp(tm.color.a, 0, 0.5f * Time.deltaTime);
            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, alpha);
        }
    }
    public void Active()
    {
        tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 1);
        tm.gameObject.SetActive(true);
        _fading = true;
    }
}
