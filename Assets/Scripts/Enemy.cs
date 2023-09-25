using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{

    public static event Action enemyDeadSequence;

    private float health;
    private float MAX_HEALTH = 100;
    private float MIN_HEALTH = 0;

    [SerializeField]
    Animator animator;

    [SerializeField]
    private Slider playerHealthSlider;
    private void OnEnable()
    {
        enemyDeadSequence += Die;
    }
    void Awake()
    {
        health = MAX_HEALTH;
        playerHealthSlider.value = health;
    }

    void Update()
    {

    }
    public void Heal(float heal)
    {
        health = Mathf.Min(MAX_HEALTH, health + heal);
        playerHealthSlider.value = health / MAX_HEALTH;
    }

    public void Damage(float damage)
    {
        health = Mathf.Max(MIN_HEALTH, health - damage);
        playerHealthSlider.value = health / MAX_HEALTH;
        if (health == 0)
        {
            //enemyDeadSequence?.invoke();
            enemyDeadSequence?.Invoke();

        }
#if DEBUG
        Debug.Log("Enemy health is reduced");
#endif
    }

    private void Die()
    {
        animator.SetTrigger("Dead");
    }

    private void OnDisable()
    {
        enemyDeadSequence -= Die;
    }
}
