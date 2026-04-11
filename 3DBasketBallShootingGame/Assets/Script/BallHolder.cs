using UnityEngine;

// --------------------------------------------------
// BallHolder Class
// --------------------------------------------------

public class BallHolder : MonoBehaviour
{
    public bool hasBall = false;

    // Ball being held
    public GameObject ballInHand;

    void Start()
    {
        if(ballInHand != null)
        {
            ballInHand.SetActive(hasBall);
        }
    }

    public void PickUp()
    {
        hasBall = true;
        if(ballInHand != null)
        {
            ballInHand.SetActive(true);
        }
    }

    public void Release()
    {
        hasBall = false;
        if(ballInHand != null)
        {
            ballInHand.SetActive(false);
        }
    }
}