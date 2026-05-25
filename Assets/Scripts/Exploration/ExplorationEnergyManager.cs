using UnityEngine;
public class ExplorationEnergyManager : MonoBehaviour
{
    public static ExplorationEnergyManager Instance;

    public int MaxEnergy = 40;

    public int CurrentEnergy = 20;
    [Header("Regen")]
    public int regenAmount = 1;
    public int regenMinutes = 5;
    private void Awake()
    {
        Instance = this;
        //CurrentEnergy = MaxEnergy;
    }
    public bool HasEnough(int amount)
    {
        return CurrentEnergy >= amount;
    }
    public bool Spend(int amount)
    {
        if (!HasEnough(amount)) return false;
        CurrentEnergy -= amount;
        return true;
    }
    public void Add(int amount)
    {
        if (CurrentEnergy >= MaxEnergy) return;
        CurrentEnergy = Mathf.Clamp( CurrentEnergy + amount, 0 , MaxEnergy);
    }
    public void Set(int value)
    {
        CurrentEnergy = value;
    }
}