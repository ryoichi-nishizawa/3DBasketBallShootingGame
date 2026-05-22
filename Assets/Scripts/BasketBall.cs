using UnityEngine;

public class BasketBall : MonoBehaviour
{
    [SerializeField]
    float destroyDelay = 30.0f;

    [SerializeField]
    float assistStrength = 5.0f;

    [SerializeField]
    Rigidbody rigidbody = null;

    Transform hoopPoint = null;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    public void SetUp(Transform _hoopPoint)
    {
        hoopPoint = _hoopPoint;
    }

    void FixedUpdate()
    {
        // Apply assist only when close (within 1m) and the ball is falling.
        float dist = Vector3.Distance(transform.position, hoopPoint.position);
        if (dist < 1.0f && rigidbody.linearVelocity.y < 0.0f)
        {
            Vector3 attraction = (hoopPoint.position - transform.position).normalized;
            rigidbody.AddForce(attraction * assistStrength);
        }
    }
}