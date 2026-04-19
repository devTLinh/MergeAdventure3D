using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public static MergeManager Instance;

    public ItemDatabase database;

    void Awake()
    {
        Instance = this;
    }

    public bool CanMerge(ItemView a, ItemView b) { 
        if (a == null || b == null) return false;
        return a.model.data.mergeGroup == b.model.data.mergeGroup && a.model.data.level == b.model.data.level; }
    public ItemView Merge(ItemView a, ItemView b, BoardSlot slot) { 
        ItemData current = a.model.data;
        ItemData next = ItemDatabase.Instance.Get(current.mergeGroup, current.level + 1);
        if (next == null) return null;
        Destroy(a.gameObject); 
        Destroy(b.gameObject);
        GameObject go = Instantiate(next.prefab);
        ItemView newView = go.GetComponent<ItemView>(); 
        newView.Init(new ItemModel(next));
        slot.SetItem(newView);
        return newView; 
    }
}