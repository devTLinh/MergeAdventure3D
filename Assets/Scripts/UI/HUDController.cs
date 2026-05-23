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
        energyText.text = EnergyManager.Instance.current.ToString();
        energySlider.value = EnergyManager.Instance.current > EnergyManager.Instance.max ? 1 : (float)EnergyManager.Instance.current / EnergyManager.Instance.max;
    }
}