using UnityEngine;
public class TreasureChest : MonoBehaviour { 
    public void Open() { 
        int roll = Random.Range(0, 100);
        if (roll < 40) {
            EnergyManager.Instance.Add(5); 
            NotificationUI.Instance.Show("Treasure: +5 Energy");
        } 
        else if (roll < 65) { 
            ExplorationEnergyManager.Instance.Add(4);
            NotificationUI.Instance.Show("Treasure: +4 Explore Energy");
        } 
        else if (roll < 85) {
            NotificationUI.Instance.Show("Treasure: Utility Item");
        }
        else if (roll < 95) {
            NotificationUI.Instance.Show("Treasure: Rare Item");
        } 
        else { 
            NotificationUI.Instance.Show("Treasure: Artifact!");
        }
        Destroy(gameObject);
    } 
}