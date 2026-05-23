using System;
using UnityEngine;
using UnityEngine.UI;
public class ExplorationHUD : MonoBehaviour { 
    [SerializeField] Text text;
    [SerializeField] Text exploreTimerText;
    [SerializeField] Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        text.text = ExplorationEnergyManager.Instance.CurrentEnergy.ToString();
        slider.value = ExplorationEnergyManager.Instance.CurrentEnergy > ExplorationEnergyManager.Instance.MaxEnergy ? 1 : (float)ExplorationEnergyManager.Instance.CurrentEnergy / ExplorationEnergyManager.Instance.MaxEnergy;
        if (ExplorationEnergyManager.Instance.CurrentEnergy >= ExplorationEnergyManager.Instance.MaxEnergy)
        {
            exploreTimerText.text = "FULL";
            return;
        }
        TimeSpan remain = EnergyRegenManager.Instance.ExploreCountdown();
        exploreTimerText.text = string.Format("{0:00}:{1:00}", remain.Minutes, remain.Seconds);
    }
}