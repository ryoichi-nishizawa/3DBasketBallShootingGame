using UnityEngine;
using UnityEngine.InputSystem;

// --------------------------------------------------
// Player Class
// --------------------------------------------------

public class Player : MonoBehaviour
{
    [SerializeField]
    float speed = 3.0f;

    PlayerInput playerInput = null;
    InputAction moveAction = null;

    void Awake()
    {
        // Get PlayerInput and InputAction.
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
        }
    }

    void Update()
    {
        if (moveAction == null)
        {
            return;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();

        // Normalize to prevent faster diagonal movement
        if (input.sqrMagnitude > 1.0f)
        {
            input.Normalize();
        }

        // Calculate movement direction relative to camera (Player body) orientation
        Vector3 move = transform.right * input.x + transform.forward * input.y;

        // Apply movement
        transform.position += move * speed * Time.deltaTime;
    }
}