using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static event Action floatingCutsceneCompleted;
    public static event Action startFloatingCutscene;



    [SerializeField]
    PlayableDirector floatingCutscene;
    public static bool floatingSequenceHasBeenPlayed = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!floatingSequenceHasBeenPlayed && floatingCutscene.state == PlayState.Paused)
        {
#if DEBUG
            Debug.Log("Timeline ended");
#endif
            floatingCutsceneCompleted?.Invoke();
            floatingSequenceHasBeenPlayed = true;

        }
    }

    void FloatingCutsceneStart()
    {
    }
}
