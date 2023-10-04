using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    //private float durationToMove = 0.5f;
    private float timeElapsed = 0f;
    [SerializeField]
    private float delayBetweenThrowing = 1f;

    private float offset = 7f;
    float angle = 0;

    [SerializeField]
    GameObject[] throwables;

    Vector3[] cutscenePositions = new Vector3[20];
    [SerializeField]
    Transform enemy;
    private float rockSpeedMultiplier = 2f;

    private void OnEnable()
    {
        GameManager.floatingCutsceneCompleted += OnFloatComplete;
        GameManager.floatingCutsceneCompleted += Throw;
    }

    IEnumerator ThrowSequence()
    {
        foreach (GameObject throwable in throwables)
        {
            yield return StartCoroutine(ThrowSingle(throwable.transform));
            //yield return new WaitForSeconds(delayBetweenThrowing);
        }
#if DEBUG
        Debug.Log("Floating sequence has been played");
#endif
        GameManager.floatingSequenceHasBeenPlayed = true;
        //if (Player.health > 50)
        {
            //foreach (GameObject throwable in throwables)
            //InitializePositions();

            //StartCoroutine(ThrowSequence());
        }
    }

    private void InitializePositions()
    {
        for (int i = 0; i < throwables.Length; i++)
        {
            throwables[i].transform.position = offset * cutscenePositions[i] + enemy.position;
            throwables[i].transform.LookAt(enemy.transform);
            throwables[i].SetActive(true);
        }
    }

    IEnumerator ThrowSingle(Transform throwable)
    {
        Vector3 initialPos = throwable.position;
        Vector3 targetPos = target.position;
        float distanceFromTarget = Vector3.Distance(initialPos, targetPos);
        //float t = timeElapsed / durationToMove;
        float t = 0;
#if DEBUG
        Debug.Log("Sequence Started");
#endif
        while (distanceFromTarget > 0)
        {
#if DEBUG
            //Debug.Log("Distance from target " + distanceFromTarget + "t is " + t);
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
            t += Time.deltaTime * rockSpeedMultiplier;
            //yield return new WaitForFixedUpdate();
            yield return null;
        }

        yield return new WaitForSeconds(delayBetweenThrowing);
    }
    private void Start()
    {
        //Throw();
        timeElapsed = 0;
        AssignVectors();
        transform.position = enemy.position;
        InitializePositions();
        Throw();

    }

    private void FixedUpdate()
    {

        transform.rotation = Quaternion.Euler(0, 0, angle * 10);
        angle += Time.fixedDeltaTime;

#if DEBUG
        //Debug.Log(angle);
#endif

    }
    void OnFloatComplete()
    {
        transform.position = enemy.position;


    }

    private void AssignVectors()
    {
        float theta = 0;
        float delta = 1f / throwables.Length;
        for (int i = 0; i < throwables.Length; i++)
        {
            theta = i * delta * 2 * 3.14f;

            cutscenePositions[i] = new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0);

#if DEBUG
            //Debug.Log("theta " + theta + "delta is " + delta);

            //Debug.Log(Mathf.Cos(theta) + "<- cos " + cutscenePositions[i]);
#endif
        }
    }

    public void Throw()
    {
        StartCoroutine(ThrowSequence());
    }
    private void OnDisable()
    {
        GameManager.floatingCutsceneCompleted -= OnFloatComplete;
        GameManager.floatingCutsceneCompleted -= Throw;
    }
}
