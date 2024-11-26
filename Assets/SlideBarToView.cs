using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideBarToView : MonoBehaviour
{
    public float moveDuration = 0.2f;  // Duration of the movement
    public float bounceAmount = 0.1f; // How high the bounce should be
    public float bounceDuration = 0.5f; // Duration of the bounce

    void Start()
    {
        // Set the target position to (0, 0)
        Vector3 targetPosition = Vector3.zero;

        // Start the movement coroutine
        StartCoroutine(MoveWithBounce(targetPosition, moveDuration, bounceAmount, bounceDuration));
    }

    IEnumerator MoveWithBounce(Vector3 target, float duration, float bounce, float bounceTime)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        // Smoothly move to the target position
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);
            yield return null;
        }

        transform.position = target; // Ensure it reaches the exact target position

        // Bounce effect: move slightly past the target and then back
        Vector3 overshootPosition = target - new Vector3(0, bounce, 0); // Add bounce upward
        elapsedTime = 0f;

        // Move up to overshoot position
        while (elapsedTime < bounceTime / 2)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(target, overshootPosition, elapsedTime / (bounceTime / 2));
            yield return null;
        }

        elapsedTime = 0f;

        // Move back to target position
        while (elapsedTime < bounceTime / 2)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(overshootPosition, target, elapsedTime / (bounceTime / 2));
            yield return null;
        }

        transform.position = target; // Ensure final position is exact
    }
}
