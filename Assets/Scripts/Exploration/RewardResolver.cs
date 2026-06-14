using UnityEngine;
public static class RewardResolver { 
    public static void Resolve(ExplorationNode node) {
        switch (node.rewardType) {
            case NodeRewardType.None:
                break;
            case NodeRewardType.Energy: 
                EnergyManager.Instance.Set(EnergyManager.Instance.current + 5); 
                NotificationUI.Instance.Show("+5 Energy");
                break;
            case NodeRewardType.ExploreEnergy:
                ExplorationEnergyManager.Instance.Set(ExplorationEnergyManager.Instance.CurrentEnergy + 3);
                NotificationUI.Instance.Show("+3 Explore"); 
                break;
            case NodeRewardType.Treasure:
                TreasureChest.Instance.Open();
                NotificationUI.Instance.Show("Chest Opened!");
                break;
            case NodeRewardType.Generator:
                GeneratorUnlockSystem.UnlockAt(node.transform.position);
                NotificationUI.Instance.Show("New generator Unlocked!");
                break;
            case NodeRewardType.RareItem:
                NotificationUI.Instance.Show("Rare Artifact");
                break;
            case NodeRewardType.RegionGate: 
                RegionTransitionSystem.OpenGate();
                NotificationUI.Instance.Show("Region Gate Opened!");
                break;
        }
    }
}