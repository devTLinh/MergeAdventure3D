using UnityEngine;
public class ItemView : MonoBehaviour {
    public ItemModel Model { get; private set; }
    public BoardSlot CurrentSlot { get; set; } 
    public void Init(ItemModel model) { 
        Model = model; 
        name = model.Data.itemName;
    } 
}