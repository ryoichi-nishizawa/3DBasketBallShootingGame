using UnityEngine;
using TMPro;

// --------------------------------------------------
// Goal Detector Class
// --------------------------------------------------

public class GoalDetector : MonoBehaviour
{
    public TextMeshProUGUI ScoreText = null;
    int score = 0;

    void Awake()
    {
        score = 0;
    }

    void Start()
    {
        UpdateScoreUI();
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
            UpdateScoreUI();
        }
    }

    void UpdateScoreUI()
    {
        if (ScoreText != null)
        {
            ScoreText.text = score.ToString();
        }
    }
}