using UnityEngine;
public class EnergyManager : MonoBehaviour { 
    public static EnergyManager Instance; 
    public int current = 100; 
    public int max = 100;
    [Header("Regen")]
    public int regenAmount = 1;
    public int regenMinutes = 2;
    void Awake() {
        Instance = this;
    } 
    public bool HasEnough(int amount) { 
        return current >= amount;
    } 
    public bool Spend(int amount) {
        if (!HasEnough(amount)) return false;
        current -= amount;
        if (current < max)
        {
            EnergyRegenManager.Instance.EnsureEnergyTimer();
        }
        return true;
    } 
    public void Add(int amount) {
        current = Mathf.Clamp(current + amount, 0 , max);
    }
    public void Set(int value)
    {
        current = value;
    }
}