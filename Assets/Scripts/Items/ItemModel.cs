public class ItemModel {
    public string RuntimeId;
    public ItemData Data;
    public ItemModel(ItemData data) { 
        RuntimeId = System.Guid.NewGuid().ToString();
        Data = data;
    } 
}