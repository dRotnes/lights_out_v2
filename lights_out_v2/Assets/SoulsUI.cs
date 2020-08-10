using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulsUI : MonoBehaviour
{
    private Slider _slider;
    private bool _decrease;
    private bool _increase;
    private float timeStartedLerping;

    public float lerpTimeDecrease;
    public float lerpTimeIncrease;
    public float maxValue;
    public SignalSend canUseSpecialAtk;
    public SignalSend specialAtkFinished;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        SetMaxSouls();
    }
    private void Update()
    {
        if (_decrease)
        {
            _increase = false;
            _slider.value = Lerp(_slider.maxValue, 0, timeStartedLerping, lerpTimeDecrease);
            if(_slider.value == 0)
            {
                _decrease = false;
                specialAtkFinished.RaiseSignal();

            }
        }
        if (_increase)
        {
            _slider.value += 1;
            if (_slider.value == maxValue)
                canUseSpecialAtk.RaiseSignal();
            _increase = false;
        }
    }
    private float Lerp(float start, float end, float timeStartedLerping, float lerpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageCompleted = timeSinceStarted / lerpTime;
        var result = Mathf.Lerp(start, end, percentageCompleted);
        return result;

    }
    public void SetMaxSouls()
    {
        _slider.maxValue = maxValue;
        _slider.value = 0;

    }
    public void UpdateSouls()
    {
        if (!_decrease)
        {
            _increase = true;
            timeStartedLerping = Time.time;
        }
    }
    public void Decrease()
    {
        _decrease = true;
        timeStartedLerping = Time.time;
    }
}
