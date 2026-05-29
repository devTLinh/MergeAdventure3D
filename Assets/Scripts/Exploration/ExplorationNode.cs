using System.Collections.Generic;
using UnityEngine;

public class ExplorationNode : MonoBehaviour
{
    [Header("Data")]
    public string nodeId;
    public bool unlocked;
    public int unlockCost = 3;
    public NodeRewardType rewardType;
    [Header("Connections")]
    public List<ExplorationNode> connectedNodes = new List<ExplorationNode>();
    [Header("Fog")]
    public List<FogBlocker> fogBlockers = new List<FogBlocker>();
    [Header("Visual")]
    public GameObject marker;
    void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        if (marker != null)
        {
            marker.SetActive(!unlocked);
        }
        foreach (var fog in fogBlockers)
        {
            if (fog == null)
                continue;
            fog.gameObject.SetActive(!unlocked);
        }
    }
    public bool CanUnlock()
    {
        if (unlocked) return false;
        if (!ExplorationEnergyManager.Instance.HasEnough(unlockCost)) return false;
        if (connectedNodes.Count == 0) return true;
        foreach (var node in connectedNodes)
        {
            if (node != null && node.unlocked)
            {
                return true;
            }
        }

        return false;
    }
    public void Unlock()
    {
        ExplorationEnergyManager.Instance.Spend(unlockCost);
        unlocked = true;
        foreach (var fog in fogBlockers)
        {
            if (fog != null)
            {
                fog.FadeOut();
            }
        }
        RewardResolver.Resolve(this);
        SceneLoader.Instance.currentMapSpawn = transform.position;
        //SaveManager.Instance.SaveGame();
        Refresh();

    }
}