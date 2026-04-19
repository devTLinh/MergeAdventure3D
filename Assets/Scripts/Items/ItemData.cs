using UnityEngine;

[CreateAssetMenu(menuName = "Game/Item")]
public class ItemData : ScriptableObject
{
    public string id;
    public string mergeGroup;
    public int level;
    public GameObject prefab;
}