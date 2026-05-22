using UnityEngine;

// --------------------------------------------------
// Goal Detector Class
// --------------------------------------------------

public class GoalDetector : MonoBehaviour
{
    [SerializeField]
    TextMeshProAnimator scoreText = null;

    int score = 0;

    void Awake()
    {
        score = 0;
    }

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.SetText(score.ToString());
        }
    }

    private void OnTriggerEnter(Collider touch)
    {
        // it collided was the ball.
        if (touch.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = touch.GetComponent<Rigidbody>();
            if (rb == null)
            {
                return;
            }

            // --- Solution 1: Ignore entry from below ---
            // Process only when Y velocity is negative (= falling)
            if (rb.linearVelocity.y > 0) 
            {
#if UNITY_EDITOR
                Debug.Log("========== Invalid : Entered from below ==========");
#endif
                return;
            }

            // --- Solution 2: Ignore rim collisions ---
            // Only trigger detection when the ball center is above the trigger center
            // (Prevents false positives from bouncing off the rim or entering sideways)
            if (touch.transform.position.y < transform.position.y)
            {
#if UNITY_EDITOR
                Debug.Log("========== Invalid : Contact from side or bottom ==========");
#endif
                return;
            }

            score++;
#if UNITY_EDITOR
            Debug.Log("========== Score Increases : "+score+" ==========");
#endif

            if (scoreText != null)
            {
                scoreText.PopinAnimation(score.ToString(), 1.5f);
            }
        }
    }
}