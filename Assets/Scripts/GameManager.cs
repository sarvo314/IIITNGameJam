using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static event Action floatingCutsceneCompleted;
    public static event Action startFloatingCutscene;


    [SerializeField]
    PlayableDirector floatingCutscene;
    public static bool floatingSequenceHasBeenPlayed = false;

    [SerializeField]
    Enemy enemy;
    [SerializeField]
    Player player;
    [SerializeField]
    Transform endSequenceEnemyPosition;
    [SerializeField]
    GameObject endSequenceGameObject;

    public static float EndSequenceStartHealth = 0.5f;


    [SerializeField]
    private AudioSource fightTheme;


    private void OnEnable()
    {
        Enemy.endSequence += EndSequenceStart;
    }
    void Update()
    {
        if (floatingCutscene.gameObject.activeInHierarchy)
            if (floatingSequenceHasBeenPlayed && floatingCutscene.state == PlayState.Paused)
            {
#if DEBUG
                Debug.Log("Timeline ended");
#endif
                //floatingCutsceneCompleted?.Invoke();
                floatingSequenceHasBeenPlayed = true;
                floatingCutscene.gameObject.SetActive(false);
                enemy.gameObject.SetActive(false);
                enemy.GetComponent<Enemy>().enabled = true;
                enemy.GetComponent<NavMeshAgent>().enabled = true;
                enemy.gameObject.transform.position = player.gameObject.transform.position;
                enemy.gameObject.SetActive(true);
                fightTheme.Play();
            }
    }

    void EndSequenceStart()
    {
        endSequenceGameObject.SetActive(true);
        Player.jumpSpeed = 20;
        Player.gravity = 20;
        enemy.GetComponent<Enemy>().enabled = false;
        enemy.GetComponent<NavMeshAgent>().enabled = false;
        Destroy(enemy.GetComponent<Rigidbody>());
        enemy.transform.position = endSequenceEnemyPosition.position;

    }
    private void OnDisable()
    {
        Enemy.endSequence -= EndSequenceStart;
    }
}
