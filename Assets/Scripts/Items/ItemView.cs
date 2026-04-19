using UnityEngine;

public class ItemView : MonoBehaviour
{
    public ItemModel model;

    public void Init(ItemModel model)
    {
        this.model = model;
    }
}