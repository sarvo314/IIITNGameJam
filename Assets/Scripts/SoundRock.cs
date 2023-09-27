using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SoundRock : MonoBehaviour
{
    GameObject enemy;
    [SerializeField]
    GameObject explosion;

    [SerializeField]
    AudioSource rockExplosion;

    [SerializeField]
    GameObject rockThrowingTimeline;

    private void OnTriggerEnter(Collider other)
    {
        explosion.transform.position = transform.position;
        explosion.SetActive(true);
        rockExplosion.Play();
        Invoke("DisableRocks", 10f);

    }
    private void DisableRocks()
    {

        transform.parent.gameObject.SetActive(false);
        rockThrowingTimeline.SetActive(true);
    }

}
