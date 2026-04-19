using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    public List<ItemData> allItems = new List<ItemData>();
    private Dictionary<string, Dictionary<int, ItemData>> lookup = new Dictionary<string, Dictionary<int, ItemData>>(); 
    void Awake() { 
        Instance = this; BuildDatabase();
    }
    void BuildDatabase() {
        lookup.Clear(); 
        foreach (var item in allItems) {
            if (!lookup.ContainsKey(item.mergeGroup)) lookup[item.mergeGroup] = new Dictionary<int, ItemData>();
            lookup[item.mergeGroup][item.level] = item;
        }
    }
    public ItemData Get(string group, int level) {
        if (lookup.ContainsKey(group) && lookup[group].ContainsKey(level)) { 
            return lookup[group][level];
        } 
        return null; 
    }
}