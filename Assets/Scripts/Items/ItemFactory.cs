using UnityEngine;
public class ItemFactory : MonoBehaviour { 
    public static ItemFactory Instance;
    void Awake() {
        Instance = this;
    }
    public ItemView SpawnToSlot(BoardSlot slot, ItemData data) {
        GameObject obj = Instantiate(data.prefab);
        ItemView view = obj.GetComponent<ItemView>();
        view.Init(new ItemModel(data)); 
        slot.SetItem(view);
        view.gameObject.AddComponent<MergePopEffect>();
        return view;
    }
}