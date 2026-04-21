using UnityEngine;
public class ExplorationManager : MonoBehaviour { 
    public static ExplorationManager Instance;
    [SerializeField] private ExplorationTile[] allTiles;
    private void Awake() {
        Instance = this;
    } 
    public void TryUnlockTile(ExplorationTile tile) {
        if (tile == null) return;
        if (!tile.isLocked) return;
        if (!ExplorationEnergyManager.Instance.Spend(tile.unlockCost)) {
            NotificationUI.Instance.Show("Not enough Explore Energy");
            return;
        }
        tile.Unlock();
        SpawnTileContent(tile);
        NotificationUI.Instance.Show("Unlocked Tile"); 
    } 
    void SpawnTileContent(ExplorationTile tile) {
        switch (tile.tileType) { 
            case ExplorationTileType.Empty: break;
            case ExplorationTileType.Treasure:
                SpawnTreasure(tile);
                break;
            case ExplorationTileType.Generator:
                SpawnGenerator(tile);
                break; 
            case ExplorationTileType.RareReward:
                EnergyManager.Instance.Add(10);
                NotificationUI.Instance.Show("Found Rare Reward +10 Energy");
                break;
        }
    } 
    void SpawnTreasure(ExplorationTile tile) { 
        GameObject chest = new GameObject("TreasureChest");
        chest.transform.position = tile.transform.position + Vector3.up * 0.5f;
        chest.AddComponent<BoxCollider>();
        chest.AddComponent<TreasureChest>();
    } 
    void SpawnGenerator(ExplorationTile tile) { 
        if (tile.generatorPrefab == null) return;
        Instantiate(tile.generatorPrefab, tile.transform.position + Vector3.up * 0.5f, Quaternion.identity);
        NotificationUI.Instance.Show("New Generator Unlocked!");
    } 
}