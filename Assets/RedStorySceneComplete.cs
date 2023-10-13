using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class RedStorySceneComplete : MonoBehaviour
{
    PlayableDirector playableDirector;
    //[SerializeField]
    private const string RED_SCENE = "RedScene";
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playableDirector.state == PlayState.Paused)
        {
            SceneManager.LoadScene(RED_SCENE);
        }
    }
}
