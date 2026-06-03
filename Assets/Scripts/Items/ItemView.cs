using UnityEngine;
public class ItemView : MonoBehaviour, IHoverInfo {
    public ItemModel Model { get; private set; }
    public BoardSlot CurrentSlot { get; set; }
    public void Init(ItemModel model)
    {
        Model = model;
        name = model.Data.itemName;
    }
    public string GetHoverText()
    {
        return Model.Data.mergeGroup +
            ": " +
            Model.Data.itemName +
            " - Level " +
            Model.Data.level +
            "\nLeft Click: Interact";
    }
}