using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Lethal : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private GameObject collisionEffect;
    [SerializeField]
    private AudioSource rockHit;

    private float rockHeight = 4f;
    //[SerializeField]
    //private Transform startPosition;
    //[SerializeField]
    //private Transform endPosition;
    //[SerializeField]
    //private float radiusOfCapsule = 1.6f;
    //[SerializeField]
    //private LayerMask rockLayerToHit;
    [SerializeField]
    private GameObject rockVisual;
    [SerializeField]
    private ExplosionSO explosionSO;
    private void OnTriggerEnter(Collider collision)
    {

        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        //Debug.Log

        //float sweepDistance = 2f;

        //if(Physics.CapsuleCast(startPosition, endPosition, radiusOfCapsule, Vector3.down, sweepDistance, rockLayerToHit))

        if (!collision.gameObject.CompareTag("Enemy"))
        {
            //collisionEffect.transform.position = transform.position;
            //we damage
            if (damageable != null)
            {
                damageable.Damage(damage);
            }
            GameObject explosion = Instantiate(explosionSO.explosion, transform.position, Quaternion.identity);
            //gameObject.transform.parent.gameObject.SetActive(false);
            rockVisual.SetActive(false);
            rockHit.Play();
            //collisionEffect.SetActive(true);


        }
        Debug.Log("takraya");
    }
}
