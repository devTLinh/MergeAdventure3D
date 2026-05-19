using UnityEngine;
public class PlayerInteraction : MonoBehaviour {
    [SerializeField] Camera cam; 
    void Update() {
        if (Input.GetMouseButtonDown(0)) Interact();
        if (Input.GetMouseButtonDown(1)) SceneLoader.Instance.LoadSceneByName("ForestCamp");
        if(Input.GetKeyDown(KeyCode.Space)) SceneLoader.Instance.ReturnBoard();
    } 
    void Interact() { 
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (!Physics.Raycast(ray, out RaycastHit hit, 4f)) return;
        if (hit.collider.TryGetComponent(out Generator gen)) { 
            gen.Use();
            return;
        }
        if (hit.collider.TryGetComponent(out DeliveryBox box)) { 
            box.Deliver(); 
            return;
        }
        //Add more interactions here
        if (hit.collider.TryGetComponent(out NodeMarker marker)) { 
            marker.Interact(); 
            return;
        }
        if (hit.collider.TryGetComponent(out TreasureChest chest)) {
            chest.Open();
            return;
        }
        //
        if (hit.collider.TryGetComponent(out ItemView item)) {
            PlayerHoldSystem.Instance.Pickup(item);
            return;
        } 
        if (hit.collider.TryGetComponent(out BoardSlot slot)) {
            PlayerHoldSystem.Instance.Place(slot);
        }
    }
}