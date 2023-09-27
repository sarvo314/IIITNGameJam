using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] punches;
    void Start()
    {

    }

    private int ChooseAPunch()
    {
        int punch = Random.Range(0, punches.Length);
        return punch;
    }
    public void PlayPunchSound()
    {
        punches[ChooseAPunch()].Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
