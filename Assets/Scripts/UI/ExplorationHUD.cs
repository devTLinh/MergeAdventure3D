using UnityEngine;
using UnityEngine.UI;
public class ExplorationHUD : MonoBehaviour { 
    [SerializeField] Text text;
    [SerializeField] Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    void Update()
    {
        text.text = ExplorationEnergyManager.Instance.CurrentEnergy.ToString();
        slider.value = ExplorationEnergyManager.Instance.CurrentEnergy > ExplorationEnergyManager.Instance.MaxEnergy ? 1 : (float)ExplorationEnergyManager.Instance.CurrentEnergy / ExplorationEnergyManager.Instance.MaxEnergy;
    }
}