using System;
using UnityEngine;
using UnityEngine.UI;
public class HUDController : MonoBehaviour {
    [SerializeField] Text energyText;
    [SerializeField] Text energyTimerText;
    [SerializeField] Slider energySlider;
    void Start()
    {
        energySlider = GetComponent<Slider>();
    }
    void Update() {
        energyText.text = EnergyManager.Instance.current.ToString();
        energySlider.value = EnergyManager.Instance.current > EnergyManager.Instance.max ? 1 : (float)EnergyManager.Instance.current / EnergyManager.Instance.max;
        if (EnergyManager.Instance.current >= EnergyManager.Instance.max)
        {
            energyTimerText.text = "FULL";
            return;
        }
        TimeSpan remain =EnergyRegenManager.Instance.EnergyCountdown();
        energyTimerText.text = string.Format("{0:00}:{1:00}", remain.Minutes, remain.Seconds);
    }
}