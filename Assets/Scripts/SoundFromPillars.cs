using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundFromPillars : MonoBehaviour
{
    GameObject[] pillars;
    [SerializeField]
    AudioSource rageSound;

    AudioSource rockSource;

    void Start()
    {
        foreach (Transform child in transform)
        {
            rockSource = child.AddComponent<AudioSource>();
            break;
        }
        rockSource.loop = true;
        rockSource.clip = rageSound.clip;
        rockSource.volume = 1f;
        rockSource.Play();
        rockSource.spatialBlend = 1;
    }

    public void DisableAllPillars()
    {
        gameObject.SetActive(false);
        //foreach (Transform child in transform)
        //{
        //    //rockSource = child.AddComponent<AudioSource>();
        //    child.gameObject.SetActive(false);
        //}
    }
}
