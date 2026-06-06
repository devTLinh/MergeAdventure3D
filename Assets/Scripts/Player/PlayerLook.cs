using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform playerBody;
    float xRotation = 0f;
    void Update() { 
        float sensitivity = GameSettingsManager.Instance.MouseSensitivity;
        if(HelpUI.Instance.isOpen) {
            sensitivity = 0f;
        }
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX); 
    }
}