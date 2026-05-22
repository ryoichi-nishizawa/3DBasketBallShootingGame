using TMPro;
using UnityEngine;
using System.Collections;

public class TextMeshProAnimator : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textMeshPro = null;

    [SerializeField]
    float duration = 0.3f;

    Coroutine _popinCoroutineCoroutine = null;

    public void SetText(string text)
    {
        textMeshPro.text = text;
    }

    public void PopinAnimation(string text, float scale = 2.0f)
    {
        if (_popinCoroutineCoroutine != null)
        {
            StopCoroutine(_popinCoroutineCoroutine);
        }

        _popinCoroutineCoroutine = StartCoroutine(PopinCoroutine(text, scale));
    }

    IEnumerator PopinCoroutine(string text, float scale)
    {
        float elapsedTime = 0.0f;
        textMeshPro.text = text;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration; 
            float EasingT = t * (2.0f - t);

            // Scale calculation.
            float currentScale = Mathf.Lerp(scale, 1.0f, EasingT);
            textMeshPro.transform.localScale = new Vector3(1.0f, currentScale, 1.0f);

            // Change Alpha from 0.0f to 1.0f.
            textMeshPro.alpha = Mathf.Lerp(0.0f, 1.0f, t);

            yield return null;
        }

        // Final adjustment.
        textMeshPro.transform.localScale = Vector3.one;
        textMeshPro.alpha = 1.0f;

        _popinCoroutineCoroutine = null;
    }
}