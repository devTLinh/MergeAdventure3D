using UnityEngine;
[CreateAssetMenu(menuName = "Merge Frontier/Item")]
public class ItemData : ScriptableObject {
    public string itemId;
    public string itemName;
    public string mergeGroup;
    public int level;
    public GameObject prefab;
}