using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToLocation : MonoBehaviour
{

    public IEnumerator StartMoving(Vector3 target, float duration, AnimationCurve curve)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float curveValue = curve.Evaluate(elapsedTime / duration); // Evaluate the curve at the current time
            transform.position = Vector3.Lerp(startPosition, target, curveValue);
            yield return null;
        }

        transform.position = target; // Ensure final position is exact
    }
}
