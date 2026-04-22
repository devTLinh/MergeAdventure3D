using UnityEngine;
public class TreasureSpawner : MonoBehaviour { 
    public static TreasureSpawner Instance;
    [SerializeField] GameObject chestPrefab;
    void Awake() { 
        Instance = this;
    } 
    public static void Spawn(Vector3 pos) {
        Instantiate(Instance.chestPrefab, pos + Vector3.up * 0.5f, Quaternion.identity);
    }
}