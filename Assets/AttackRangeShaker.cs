using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeShaker : MonoBehaviour
{
    public float shakeDuration = 1.0f; // Total time to shake
    public float shakeMagnitude = 0.1f; // Intensity of the shake
    private Vector3 originalPosition; // Store the original position
    private float shakeTimer = 0f;

    private void Start()
    {
        // Save the original position of the bar
        originalPosition = transform.localPosition;
    }

    // public void StartShaking(float duration, float magnitude)
    public void StartShaking()
    {
        // shakeDuration = duration;
        // shakeMagnitude = magnitude;
        shakeTimer = shakeDuration; // Reset the timer
    }

    private void Update()
    {
        if (shakeTimer > 0)
        {
            // Apply random offset to simulate shaking
            transform.localPosition = originalPosition + (Vector3)Random.insideUnitCircle * shakeMagnitude;

            // Reduce the timer
            shakeTimer -= Time.deltaTime;

            // Stop shaking and reset position when done
            if (shakeTimer <= 0)
            {
                transform.localPosition = originalPosition;
            }
        }
    }
}