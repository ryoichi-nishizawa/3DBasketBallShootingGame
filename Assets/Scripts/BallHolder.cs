using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

// --------------------------------------------------
// BallHolder Class
// --------------------------------------------------

public class BallHolder : MonoBehaviour
{
    public bool HasBall { get; private set; } = false;

    [SerializeField]
    Transform floorTransform = null;

    [SerializeField]
    Player player = null;

    // Ball being held
    [SerializeField]
    GameObject ballInHand = null;

    Mouse mouse = null;
    Coroutine PickupAndPrepareCoroutine = null;

    [SerializeField]
    float moveDuration = 0.3f;

    void Awake()
    {
        mouse = Mouse.current;
    }

    public void PickUp()
    {
        PickupAndPrepareCoroutine = StartCoroutine(PickupAndPrepareRoutine());
    }

    public void Release()
    {
        if (PickupAndPrepareCoroutine != null)
        {
            StopCoroutine(PickupAndPrepareCoroutine);
            PickupAndPrepareCoroutine = null;
        }

        HasBall = false;
        ballInHand.SetActive(false);
    }

    IEnumerator PickupAndPrepareRoutine()
    {
        yield return new WaitUntil(() => mouse.leftButton.wasPressedThisFrame);
        yield return null;

        // Spawn a ball at the player's feet.
        Vector3 playerBasePos = player.transform.position + transform.forward * 0.5f;
        playerBasePos.y = floorTransform.position.y + 0.4f;

        Vector3 startPos = playerBasePos + (player.transform.forward * 0.5f);
        ballInHand.transform.parent = null;
        ballInHand.transform.position = startPos;
        ballInHand.SetActive(true);

        yield return new WaitUntil(() => mouse.leftButton.wasPressedThisFrame);
        yield return null;

        // Picking up the ball and bringing it to hand.
        float elapsed = 0.0f;
        while (elapsed < moveDuration)
        {
            ballInHand.transform.position = Vector3.Lerp(startPos, transform.position, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Final adjustments
        ballInHand.transform.parent = transform;
        ballInHand.transform.localPosition = Vector3.zero;
        ballInHand.transform.localRotation = Quaternion.identity;

        HasBall = true;
        PickupAndPrepareCoroutine = null;
    }
}