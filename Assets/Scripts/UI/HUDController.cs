using UnityEngine;
using UnityEngine.UI;
public class HUDController : MonoBehaviour {
    [SerializeField] Text energyText;
    [SerializeField] Slider energySlider;
    void Start()
    {
        energySlider = GetComponent<Slider>();
    }
    void Update() {
        energyText.text = EnergyManager.Instance.CurrentEnergy.ToString();
        energySlider.value = EnergyManager.Instance.CurrentEnergy > EnergyManager.Instance.MaxEnergy ? 1 : (float)EnergyManager.Instance.CurrentEnergy / EnergyManager.Instance.MaxEnergy;
    }
}