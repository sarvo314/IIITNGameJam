using System.Collections;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 2.0f;
    public float moveSpeed = 5.0f;
    public float delayBetweenMoves = 2.0f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + Vector3.up * moveDistance;
        StartCoroutine(MoveObstacle());
    }

    private IEnumerator MoveObstacle()
    {
        while (true)
        {
            // Move the obstacle up
            yield return StartCoroutine(MoveToPosition(transform, targetPosition));

            // Pause for a delay
            float randDelay = Random.Range(0, delayBetweenMoves);
            yield return new WaitForSeconds(randDelay);

            // Move the obstacle down
            yield return StartCoroutine(MoveToPosition(transform, originalPosition));

            // Pause for a delay
            yield return new WaitForSeconds(randDelay);
        }
    }

    private IEnumerator MoveToPosition(Transform objectToMove, Vector3 targetPosition)
    {
        float journeyLength = Vector3.Distance(objectToMove.position, targetPosition);
        float startTime = Time.time;


        while (objectToMove.position != targetPosition)
        {
            float distanceCovered = (Time.time - startTime) * moveSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            objectToMove.position = Vector3.Lerp(objectToMove.position, targetPosition, fractionOfJourney);
            yield return null;
        }
    }

}
