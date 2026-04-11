using UnityEngine;
using UnityEngine.InputSystem;

// --------------------------------------------------
// Mouse Controler Class
// --------------------------------------------------

public class MouseControler : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 10.0f;

    [SerializeField]
    Transform playerBody = null;

    float xRotation = 0.0f;

    void Start()
    {
        xRotation = 0.0f;
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
            float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

            // Calculate vertical rotation (pitch)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

            // Apply rotation to the camera
            transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

            // Rotate the player character horizontally (yaw)
            playerBody.Rotate(Vector3.up * mouseX);
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            // Enable free cursor movement when right-click is released
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}