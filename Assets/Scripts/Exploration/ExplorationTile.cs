using UnityEngine;
public class ExplorationTile : MonoBehaviour {
    public Vector2Int gridPos;
    public bool isLocked = true; 
    public int unlockCost = 3;
    public ExplorationTileType tileType;
    [Header("Optional Spawn")] public Generator generatorPrefab;
    [Header("Visual")][SerializeField] private GameObject fogVisual;
    [SerializeField] private GameObject unlockedGround;
    public void RefreshVisual() { 
        fogVisual.SetActive(isLocked);
        unlockedGround.SetActive(!isLocked);
    }
    private void Start() { 
        RefreshVisual();
    } 
    public void Unlock() { 
        isLocked = false;
        RefreshVisual();
    } 
}