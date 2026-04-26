using UnityEngine;
public class TestSpawner : MonoBehaviour { 
    public BoardSlot slotA;
    public BoardSlot slotB;
    void Start() { 
        Spawn(slotA);
        Spawn(slotB);
    } 
    void Spawn(BoardSlot slot) {
        ItemData data = ItemDatabase.Instance.Get("Tool", 4);
        Debug.Log($"Spawning {data.itemName} in {slot.name}");
        GameObject go = Instantiate(data.prefab);
        ItemView view = go.GetComponent<ItemView>();
        view.Init(new ItemModel(data));
        slot.SetItem(view); 
    } 
}