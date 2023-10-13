using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThereYouAreTrigger : MonoBehaviour
{
    [SerializeField]
    AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioManager.ThereYouAre();
        }
    }

}
