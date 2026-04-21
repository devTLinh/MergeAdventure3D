using UnityEngine;
public class ExplorationEnergyManager : MonoBehaviour {
    public static ExplorationEnergyManager Instance;
    [SerializeField] private int startEnergy = 10;
    public int Current { get; private set; }
    private void Awake() { 
        Instance = this;
        Current = startEnergy;
    }
    public bool HasEnough(int amount) {
        return Current >= amount;
    } 
    public bool Spend(int amount) {
        if (!HasEnough(amount)) return false;
        Current -= amount;
        return true;
    }
    public void Add(int amount) {
        Current += amount; 
    } 
}