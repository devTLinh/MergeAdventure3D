using UnityEngine;
public class EnergyManager : MonoBehaviour { 
    public static EnergyManager Instance; 
    public int CurrentEnergy = 20; 
    public int MaxEnergy = 100;
    void Awake() {
        Instance = this;
    } 
    public bool HasEnough(int amount) { 
        return CurrentEnergy >= amount;
    } 
    public bool Spend(int amount) {
        if (!HasEnough(amount)) return false;
        CurrentEnergy -= amount;
        return true;
    } 
    public void Add(int amount) { 
        CurrentEnergy += amount;
    }
    public void Set(int value)
    {
        CurrentEnergy = value;
    }
}