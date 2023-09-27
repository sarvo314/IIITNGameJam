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
    private AudioSource fightTheme;



    void Start()
    {

    }

    // Update is called once per frame
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

    void FloatingCutsceneStart()
    {
    }
}
