using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    public Vector2Int pos;
    public ItemView currentItem { get; private set; }

    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public void SetItem(ItemView item)
    {
        currentItem = item;
        item.CurrentSlot = this;
        item.transform.SetParent(transform);
        item.transform.position = transform.position + Vector3.up * 0.5f;
        item.transform.localRotation = Quaternion.identity;
    }

    public void Clear()
    {
        currentItem = null;
    }
}