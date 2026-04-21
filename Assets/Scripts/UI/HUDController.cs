using UnityEngine;
using UnityEngine.UI;
public class HUDController : MonoBehaviour {
    [SerializeField] Text energyText;
    void Update() {
        energyText.text = "Energy: " + EnergyManager.Instance.CurrentEnergy;
    }
}