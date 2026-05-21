using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

// --------------------------------------------------
// Shoot Controller Class
// --------------------------------------------------

public class ShootController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    GameObject ballPrefab = null;

    [SerializeField]
    Transform shootPoint = null;

    [SerializeField]
    Transform hoopPoint = null;

    [SerializeField]
    TrajectoryPredictor trajectoryPredictor = null;

    [SerializeField]
    Slider powerSlider = null;

    BallHolder ballHolder = null;

    [Header("Settings")]
    [SerializeField]
    float maxPower = 20.0f;

    [Header("Mouse Settings")]
    [SerializeField]
    float mouseSensitivity = 0.005f;

    Mouse mouse = null;
    Vector2 startMousePosition = Vector2.zero;
    bool isCharging = false;

    void Awake()
    {
        mouse = Mouse.current;
        ballHolder = GetComponent<BallHolder>();
    }

    void Start()
    {
        if (powerSlider != null)
        {
            powerSlider.gameObject.SetActive(false);
        }

        // Hold the ball
        ballHolder.PickUp();
    }

    void Update()
    {
        if (!Application.isFocused)
        {
            return;
        }

        // If not holding a ball, do not allow charging
        if (ballHolder != null && !ballHolder.HasBall)
        {
            return;
        }

        // When left-click is pressed
        if (mouse.leftButton.wasPressedThisFrame)
        {
            isCharging = true;
            startMousePosition = mouse.position.ReadValue();
            powerSlider.gameObject.SetActive(true);
        }

        // Charge
        if (isCharging)
        {
            Vector2 currentMousePosition = mouse.position.ReadValue();

            // Calculate power based on downward pull (Y-axis delta)
            float diffY = startMousePosition.y - currentMousePosition.y;

            // Charge power
            float powerRatio = Mathf.Clamp(diffY * mouseSensitivity, 0.0f, 1.0f);
            powerSlider.value = powerRatio;

            // When left-click is released
            if (mouse.leftButton.wasReleasedThisFrame)
            {
                Shoot(powerRatio);

                // Release the ball
                if (ballHolder != null)
                {
                    ballHolder.Release();
                }

                isCharging = false;
                powerSlider.gameObject.SetActive(false);
                trajectoryPredictor.gameObject.SetActive(false);
            }
            else
            {
                // Apply force forward from the camera + slightly upward
                Vector3 shootDir = transform.forward + Vector3.up * 1.5f;

                // Display trajectory
                trajectoryPredictor.gameObject.SetActive(true);
                trajectoryPredictor.ShowTrajectory(shootPoint.position, shootDir.normalized * (powerRatio * maxPower));
            }
        }
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            // Hold the ball
            ballHolder.PickUp();
        }
        else
        {
            // Release the ball
            if (ballHolder != null)
            {
                ballHolder.Release();
            }

            isCharging = false;
            powerSlider.gameObject.SetActive(false);
            trajectoryPredictor.gameObject.SetActive(false);
        }
    }

    void Shoot(float powerRatio)
    {
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        BasketBall basketBall = ball.GetComponent<BasketBall>();
        basketBall.SetUp(hoopPoint);

        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Apply force forward from the camera + slightly upward
        Vector3 shootDir = transform.forward + Vector3.up * 1.5f;
        rb.AddForce(shootDir.normalized * (powerRatio * maxPower), ForceMode.Impulse);

        // Hold a ball again after a few seconds
        StartCoroutine(RechargeBallRoutine(2.0f));
    }

    IEnumerator RechargeBallRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ballHolder.PickUp();
    }
}