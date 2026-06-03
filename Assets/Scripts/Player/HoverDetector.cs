using UnityEngine;

public class HoverDetector :
MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    LayerMask interactableMask;

    [SerializeField]
    float distance = 4f;

    string currentText;

    void Update()
    {
        Ray ray =
            new Ray(
                cam.transform.position,
                cam.transform.forward);

        if (!Physics.Raycast(
            ray,
            out RaycastHit hit,
            distance,
            interactableMask))
        {
            ClearHover();
            return;
        }

        if (hit.collider.TryGetComponent(
            out IHoverInfo hover))
        {
            string text =
                hover.GetHoverText();

            if (text != currentText)
            {
                currentText = text;

                HoverUI.Instance.Show(
                    currentText);
            }

            return;
        }

        ClearHover();
    }

    void ClearHover()
    {
        if (string.IsNullOrEmpty(
            currentText))
            return;

        currentText = null;

        HoverUI.Instance.Hide();
    }
}