using UnityEngine;
public class BoardManager : MonoBehaviour {
    public static BoardManager Instance;
    public BoardSlot[] slots;
    void Awake() { 
        Instance = this;
    } 
    public BoardSlot GetFirstEmpty() {
        foreach (var slot in slots) {
            if (slot.IsEmpty()) return slot; 
        }
        return null;
    } 
}