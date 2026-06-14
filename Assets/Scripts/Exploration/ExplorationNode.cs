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
        if (!ExplorationEnergyManager.Instance.HasEnough(unlockCost)) 
        {
            NotificationUI.Instance.Show("Need Explore Energy");
            return false;
        }
        if (connectedNodes.Count == 0) return true;
        foreach (var node in connectedNodes)
        {
            if (node != null && node.unlocked)
            {
                return true;
            }
        }
        NotificationUI.Instance.Show("Path Locked");
        return false;
    }
    public void Unlock()
    {
        unlocked = true;
        foreach (var fog in fogBlockers)
        {
            if (fog != null)
            {
                fog.FadeOut();
            }
        }
        SceneLoader.Instance.currentMapSpawn = transform.position;
        RewardResolver.Resolve(this);
        TutorialManager.Instance.Notify(TutorialStep.UnlockNode);
        //SaveManager.Instance.SaveGame();
        Refresh();

    }
}