using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GreenSceneComplete : MonoBehaviour
{
    PlayableDirector playableDirector;
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playableDirector.state == PlayState.Paused)
        {
            SceneManager.LoadScene(1);
        }
    }
}
