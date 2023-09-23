using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    private float health;
    private float MAX_HEALTH = 100;
    private float MIN_HEALTH = 0;

    void Awake()
    {
        health = MAX_HEALTH;
    }

    void Update()
    {

    }
    public void Heal(float heal)
    {
        health = Mathf.Min(MAX_HEALTH, health + heal);
    }

    public void Damage(float damage)
    {
        health = Mathf.Max(MIN_HEALTH, health - damage);
#if DEBUG
        Debug.Log("Player health is reduced");
#endif
    }
}
