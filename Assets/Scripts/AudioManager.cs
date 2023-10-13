using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] punches;
    [SerializeField]
    private AudioSource getHit;
    [SerializeField]
    private AudioSource rage;
    [SerializeField]
    private AudioSource fireball;
    [SerializeField]
    private AudioSource mainTheme;
    [SerializeField]
    private AudioSource fightTheme;
    [SerializeField]
    private AudioSource iWillDestroyThisWorld;

    void Start()
    {
        mainTheme.volume = 0.1f;
        mainTheme.Play();
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

    public void PlayGetHitSound()
    {
        getHit.Play();
    }
    public void PlayRageSound()
    {
        rage.Play();
    }
    public void PlayFireballSound()
    {
        fireball.Play();
    }
    public void IWillDestroyThisWorld()
    {
        iWillDestroyThisWorld.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.floatingSequenceHasBeenPlayed)
        {

            mainTheme.Stop();
            Debug.Log("We play fight theme");
            //fightTheme.Play();
        }

    }
}
