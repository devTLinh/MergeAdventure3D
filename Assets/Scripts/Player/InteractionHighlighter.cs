using UnityEngine;
public class InteractionHighlighter : MonoBehaviour { 
    public Camera cam;
    public float range = 4f;
    Renderer lastRenderer;
    void Update() { 
        Ray ray = new Ray(cam.transform.position, cam.transform.forward); 
        if (Physics.Raycast(ray, out RaycastHit hit, range)) {
            Renderer r = hit.collider.GetComponent<Renderer>();
            if (r != lastRenderer) { 
                ClearLast();
                lastRenderer = r;
                if (r != null) r.material.color = Color.yellow;
            }
        } 
        else { 
            ClearLast();
        }
    } 
    void ClearLast() {
        if (lastRenderer != null) {
            lastRenderer.material.color = Color.white; 
            lastRenderer = null; 
        }
    } 
}