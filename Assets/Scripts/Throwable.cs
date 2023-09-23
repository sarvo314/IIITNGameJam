using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float durationToMove = 2f;
    private float timeElapsed = 0f;
    [SerializeField]
    private float delayBetweenThrowing = 2f;

    [SerializeField]
    GameObject[] throwables;

    private void OnEnable()
    {
        GameManager.floatingCutsceneCompleted += Throw;
    }

    IEnumerator ThrowSequence()
    {
        foreach (GameObject throwable in throwables)
        {
            yield return StartCoroutine(ThrowSingle(throwable.transform));
            yield return new WaitForSeconds(delayBetweenThrowing);
        }
#if DEBUG
        Debug.Log("Floating sequence has been played");
#endif
        GameManager.floatingSequenceHasBeenPlayed = true;
    }

    IEnumerator ThrowSingle(Transform throwable)
    {
        Vector3 initialPos = throwable.position;
        Vector3 targetPos = target.position;
        float distanceFromTarget = Vector3.Distance(initialPos, targetPos);
        float t = timeElapsed / durationToMove;
#if DEBUG
        Debug.Log("Sequence Started");
#endif
        while (distanceFromTarget > 0)
        {
#if DEBUG
            Debug.Log("Distance from target " + distanceFromTarget + "t is " + t);
#endif

            throwable.position = Vector3.Lerp(initialPos, targetPos, t);
            if (t < 1)
            {
                distanceFromTarget = Vector3.Distance(throwable.position, targetPos);
            }
            else
            {
                throwable.position = targetPos;
                distanceFromTarget = 0;
                t = 1;
                timeElapsed = 0;
            }
            t += Time.deltaTime;
            //yield return new WaitForFixedUpdate();
            yield return null;
        }

        yield break;
    }
    private void Awake()
    {
        //Throw();
        timeElapsed = 0;
    }

    public void Throw()
    {
        StartCoroutine(ThrowSequence());
    }
    private void OnDisable()
    {
        GameManager.floatingCutsceneCompleted -= Throw;
    }
}
