using UnityEngine;

// --------------------------------------------------
// BallHolder Class
// --------------------------------------------------

public class BallHolder : MonoBehaviour
{
    public bool HasBall { get; private set; } = false;

    // Ball being held
    [SerializeField]
    GameObject ballInHand = null;

    public void PickUp()
    {
        HasBall = true;
        if(ballInHand != null)
        {
            ballInHand.SetActive(true);
        }
    }

    public void Release()
    {
        HasBall = false;
        if(ballInHand != null)
        {
            ballInHand.SetActive(false);
        }
    }
}