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
    public GameObject ballPrefab;
    public Transform shootPoint;
    public Slider powerSlider;
    private BallHolder ballHolder;

    [Header("Settings")]
    public float maxPower = 25.0f;
    public float chargeSpeed = 1.0f;

    private float currentPower = 0.0f;
    private bool isCharging = false;

    void Awake()
    {
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
        // If not holding a ball, do not allow charging
        if (ballHolder != null && !ballHolder.hasBall)
        {
            return;
        }

        var mouse = Mouse.current;
        if (mouse == null)
        {
            return;
        }

        // When left-click is pressed
        if (mouse.leftButton.wasPressedThisFrame)
        {
            isCharging = true;
            currentPower = 0f;
            powerSlider.gameObject.SetActive(true);
        }

        // Charge
        if (isCharging)
        {
            // Charge power
            currentPower += Time.deltaTime * chargeSpeed;
            float displayPower = Mathf.PingPong(currentPower, 1f);
            powerSlider.value = displayPower;

            // When left-click is released
            if (mouse.leftButton.wasReleasedThisFrame)
            {
                Shoot(displayPower);

                // Release the ball
                if (ballHolder != null)
                {
                    ballHolder.Release();
                }

                isCharging = false;
                powerSlider.gameObject.SetActive(false);
            }
        }
    }

    void Shoot(float powerRatio)
    {
        GameObject ball = Instantiate(ballPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        // Apply force forward from the camera + slightly upward
        Vector3 shootDir = transform.forward + Vector3.up * 0.5f;
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