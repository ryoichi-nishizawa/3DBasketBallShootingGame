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
            score++;
            Debug.Log("========== score increases : "+score+" ==========");
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