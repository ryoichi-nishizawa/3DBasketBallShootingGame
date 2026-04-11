using UnityEngine;
using UnityEngine.InputSystem;

// --------------------------------------------------
// Mouse Controler Class
// --------------------------------------------------

public class MouseControler : MonoBehaviour
{
    public float MouseSensitivity = 0.1f;
    public Transform PlayerBody = null;
    float xRotation = 0.0f;

    void Start()
    {
        xRotation = 0.0f;
    }

    void Update()
    {
        // Check for right-click input
        if (Mouse.current.rightButton.isPressed)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            float mouseX = mouseDelta.x * MouseSensitivity;
            float mouseY = mouseDelta.y * MouseSensitivity;

            // Calculate vertical rotation (pitch)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply rotation to the camera
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotate the player character horizontally (yaw)
            PlayerBody.Rotate(Vector3.up * mouseX);
        }
        else
        {
            // Enable free cursor movement when right-click is released
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}