using UnityEngine;
using UnityEngine.UI;
//Add
public class ExplorationHUD : MonoBehaviour { 
    [SerializeField] Text textUI;
    void Update() {
        textUI.text = "Explore: " + ExplorationEnergyManager.Instance.Current;
    }
}