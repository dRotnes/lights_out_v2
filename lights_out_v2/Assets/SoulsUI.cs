using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SoulsUI : MonoBehaviour
{
    private bool _decrease;
    private bool _increase;
    private float timeStartedLerping;

    public Slider slider;
    public float lerpTimeDecrease;
    public float lerpTimeIncrease;
    public float maxValue;
    public SignalSend canUseSpecialAtk;
    public SignalSend specialAtkFinished;

    private void Start()
    {
        SetMaxSouls();
    }
    private void Update()
    {
        if (_decrease)
        {
            _increase = false;
            slider.value = Lerp(slider.maxValue, 0, timeStartedLerping, lerpTimeDecrease);
            if(slider.value == 0)
            {
                _decrease = false;
                specialAtkFinished.RaiseSignal();

            }
        }
        if (_increase)
        {
            slider.value += 1;
            if (slider.value == maxValue)
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
        slider.maxValue = maxValue;
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
