using UnityEngine;
using UnityEngine.InputSystem;

// --------------------------------------------------
// Mouse Controller Class
// --------------------------------------------------

public class MouseController : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 7.5f;

    [SerializeField]
    Transform playerBody = null;

    Mouse mouse = null;
    float xRotation = 0.0f;

    void Awake()
    {
        mouse = Mouse.current;
    }

    void Start()
    {
        xRotation = 0.0f;
    }

    void LateUpdate()
    {
        if (!Application.isFocused)
        {
            return;
        }

        if (mouse.rightButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (mouse.rightButton.isPressed)
        {
            Vector2 mouseDelta = mouse.delta.ReadValue();
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
        else if (mouse.rightButton.wasReleasedThisFrame)
        {
            // Enable free cursor movement when right-click is released
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}