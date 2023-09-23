using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lethal : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    GameObject collisionEffect;
    private void OnTriggerEnter(Collider other)
    {


        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        collisionEffect.transform.position = other.transform.position;
        if (damageable != null)
        {
            damageable.Damage(damage);
        }
        if (!other.gameObject.CompareTag("Enemy"))
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            //gameObject.SetActive(false);
            collisionEffect.SetActive(true);

        }


    }
}
