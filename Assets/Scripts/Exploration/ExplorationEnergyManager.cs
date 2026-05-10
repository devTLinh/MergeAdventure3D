using UnityEngine;
public class ExplorationEnergyManager : MonoBehaviour
{
    public static ExplorationEnergyManager Instance;

    public int MaxEnergy = 40;

    public int CurrentEnergy = 20;
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
        CurrentEnergy += amount;
    }
    public void Set(int value)
    {
        CurrentEnergy = value;
    }
}