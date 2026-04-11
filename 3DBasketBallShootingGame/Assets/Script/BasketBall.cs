using UnityEngine;

public class BasketBall : MonoBehaviour
{
    [SerializeField]
    float destroyDelay = 30.0f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }
}