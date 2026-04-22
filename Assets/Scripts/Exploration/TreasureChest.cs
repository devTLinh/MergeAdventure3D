using UnityEngine;
public class TreasureChest : MonoBehaviour {
    public void Open() {
        int roll = Random.Range(0, 100);
        if (roll < 40) {
            EnergyManager.Instance.Add(5); 
            NotificationUI.Instance.Show("Chest +5 Energy");
        } 
        else if (roll < 70) {
            ExplorationEnergyManager.Instance.Add(4);
            NotificationUI.Instance.Show("Chest +4 Explore");
        } 
        else if (roll < 90) { 
            NotificationUI.Instance.Show("Utility Item"); 
        } 
        else {
            NotificationUI.Instance.Show("Rare Item");
        }
        Destroy(gameObject);
    }
}