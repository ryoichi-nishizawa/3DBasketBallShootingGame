using UnityEngine;

public class TrajectoryPredictor : MonoBehaviour
{
    [SerializeField]
    LineRenderer lineRenderer = null;

    [SerializeField]
    int count = 30;

    [SerializeField]
    float spacing = 0.5f;

    public void ShowTrajectory(Vector3 startPoint, Vector3 initialVelocity)
    {
        lineRenderer.positionCount = count;

        // Adjusting trajectory width
        lineRenderer.startWidth = 0.06f;
        lineRenderer.endWidth = 0.03f;

        Vector3[] points = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            float t = i * spacing;

            // P = P0 + V0 * t + 0.5 * g * t^2
            Vector3 pos = startPoint + initialVelocity * t + 0.5f * Physics.gravity * t * t;
            points[i] = pos;
        }

        lineRenderer.SetPositions(points);
    }
}