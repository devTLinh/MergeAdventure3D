using UnityEngine;
using UnityEngine.UI;
public class ExplorationHUD : MonoBehaviour { 
    [SerializeField] Text textUI;
    void Update() {
        textUI.text = "Explore: " + ExplorationEnergyManager.Instance.Current;
    }
}