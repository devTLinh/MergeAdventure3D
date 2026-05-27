using UnityEngine;
using System;
using System.Collections;
using System.Globalization;
public class EnergyRegenManager : MonoBehaviour{
    public static EnergyRegenManager Instance;
    DateTime nextEnergyTime;
    DateTime nextExploreTime;
    void Awake(){
        Instance = this;
    }
    void Start(){
        StartCoroutine(RegenTickRoutine());
    }
    IEnumerator RegenTickRoutine(){
        while (true)
        {
            TickEnergy();
            TickExplore();

            yield return new WaitForSeconds(1f);
        }
    }
    void TickEnergy(){
        if (nextEnergyTime == default) return;
        if (EnergyManager.Instance.current >= EnergyManager.Instance.max)
        {
            nextEnergyTime = default;
            return;
        }
        int safety = 0;
        while (DateTime.UtcNow >= nextEnergyTime && safety < EnergyManager.Instance.max)
        {
            EnergyManager.Instance.Add(EnergyManager.Instance.regenAmount);
            nextEnergyTime = nextEnergyTime.AddMinutes(EnergyManager.Instance.regenMinutes);
            safety++;
        }
    }
    void TickExplore(){
        if (nextExploreTime == default) return;
        if (ExplorationEnergyManager.Instance.CurrentEnergy >= ExplorationEnergyManager.Instance.MaxEnergy)
        {
            nextExploreTime = default;
            return;
        }
        int safety = 0;
        while (DateTime.UtcNow >= nextExploreTime && safety < ExplorationEnergyManager.Instance.MaxEnergy)
        {
            ExplorationEnergyManager.Instance.Add( ExplorationEnergyManager.Instance.regenAmount);
            nextExploreTime = nextExploreTime.AddMinutes(ExplorationEnergyManager.Instance.regenMinutes);
            safety++;
        }
    }
    public void ApplyOfflineRegen(GameSaveData data){
        ApplyEnergyOffline(data);
        ApplyExploreOffline(data);
        SetupNextTimers(data);
    }
    void ApplyEnergyOffline(GameSaveData data){
        if (string.IsNullOrEmpty(data.energyTimestamp))return;
        DateTime last = DateTime.Parse(data.energyTimestamp, null, DateTimeStyles.RoundtripKind);
        TimeSpan elapsed = DateTime.UtcNow - last;
        if (elapsed.TotalMinutes < 0){
            elapsed = TimeSpan.Zero;
        }
        int gained =(int)(elapsed.TotalMinutes / EnergyManager.Instance.regenMinutes);
        int amount = gained * EnergyManager.Instance.regenAmount;
        EnergyManager.Instance.Add(amount);
        Debug.Log("Offline Energy +" + amount);
    }
    void ApplyExploreOffline(GameSaveData data){
        if (string.IsNullOrEmpty(data.exploreTimestamp))return;
        DateTime last = DateTime.Parse(data.exploreTimestamp, null, DateTimeStyles.RoundtripKind);
        TimeSpan elapsed = DateTime.UtcNow - last;
        if (elapsed.TotalMinutes < 0){
            elapsed = TimeSpan.Zero;
        }
        int gained =(int)(elapsed.TotalMinutes / ExplorationEnergyManager.Instance.regenMinutes);
        int amount = gained * ExplorationEnergyManager.Instance.regenAmount;
        ExplorationEnergyManager.Instance.Add(amount);
        Debug.Log("Offline Explore +" + amount);
    }
    void SetupNextTimers(GameSaveData data){
        if (!string.IsNullOrEmpty(data.energyTimestamp))
        {
            DateTime last = DateTime.Parse(data.energyTimestamp, null, DateTimeStyles.RoundtripKind);
            TimeSpan elapsed = DateTime.UtcNow - last;
            if (elapsed.TotalMinutes < 0){
                elapsed = TimeSpan.Zero;
            }
            double cycle = EnergyManager.Instance.regenMinutes;
            double remain = cycle - (elapsed.TotalMinutes % cycle);
            if (remain >= cycle){
                remain = 0;
            }
            nextEnergyTime = DateTime.UtcNow.AddMinutes(remain);
        }
        if (!string.IsNullOrEmpty(data.exploreTimestamp))
        {
            DateTime last = DateTime.Parse(data.exploreTimestamp, null, DateTimeStyles.RoundtripKind);
            TimeSpan elapsed = DateTime.UtcNow - last;
            if (elapsed.TotalMinutes < 0){
                elapsed = TimeSpan.Zero;
            }
            double cycle = ExplorationEnergyManager.Instance.regenMinutes;
            double remain = cycle - (elapsed.TotalMinutes % cycle);
            if (remain >= cycle){
                remain = 0;
            }
            nextExploreTime = DateTime.UtcNow.AddMinutes(remain);
        }
        Debug.Log(
            "Next Energy = "
            + nextEnergyTime
            + "\nNext Explore = "
            + nextExploreTime);
    }
    public void StartRealtimeTimers()
    {
        nextEnergyTime = DateTime.UtcNow.AddMinutes(EnergyManager.Instance.regenMinutes);
        nextExploreTime = DateTime.UtcNow.AddMinutes(ExplorationEnergyManager.Instance.regenMinutes);
        Debug.Log("Realtime Regen Started");
    }
    public void EnsureEnergyTimer()
    {
        if (nextEnergyTime == default)
        {
            nextEnergyTime = DateTime.UtcNow.AddMinutes(EnergyManager.Instance.regenMinutes);
        }
    }
    public void EnsureExploreEnergyTimer()
    {
        if (nextExploreTime == default)
        {
            nextExploreTime = DateTime.UtcNow.AddMinutes(ExplorationEnergyManager.Instance.regenMinutes);
        }
    }
    public TimeSpan EnergyCountdown(){
        TimeSpan remain = nextEnergyTime - DateTime.UtcNow;
        if (remain.TotalSeconds < 0){
            remain = TimeSpan.Zero;
        }
        return remain;
    }

    public TimeSpan ExploreCountdown(){
        TimeSpan remain = nextExploreTime - DateTime.UtcNow;

        if (remain.TotalSeconds < 0){
            remain =  TimeSpan.Zero;
        }
        return remain;
    }
}