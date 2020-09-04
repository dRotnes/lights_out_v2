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
    private ControllerManager _cManager;

    public float lerpTimeDecrease;
    public float lerpTimeIncrease;
    public float maxValue;

    public Slider slider;
    public SignalSend canUseSpecialAtk;
    public SignalSend canUseSpecialAtkController;
    public SignalSend specialAtkFinished;
    public Player playerStats;

    private void Start()
    {
        _cManager = FindObjectOfType<ControllerManager>();
        SetMaxSouls();
    }
    private void Update()
    {
        slider.value = playerStats.souls;
        if (_decrease)
        {
            _increase = false;
            playerStats.souls = Lerp(slider.maxValue, 0, timeStartedLerping, lerpTimeDecrease);
            if(playerStats.souls == 0)
            {
                _decrease = false;
                specialAtkFinished.RaiseSignal();

            }
        }
        if (_increase)
        {
            playerStats.souls += 1;
            if (playerStats.souls == maxValue)
                switch (_cManager.controllerOn)
                {
                    case true:
                        canUseSpecialAtkController.RaiseSignal();
                        break;
                    case false:
                        canUseSpecialAtk.RaiseSignal();
                        break;
                }
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
