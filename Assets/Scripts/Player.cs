using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamageable
{
    public static event Action playerDeadSequence;

    public static float health;
    public static bool playerIsHitting;
    private float MAX_HEALTH = 100;
    private float MIN_HEALTH = 0;
    [SerializeField]
    private Slider playerHealthSlider;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float punchRaycastDistance = 2f;

    [SerializeField] private AudioSource[] punches;

    //damage by punches
    [SerializeField]
    private float damage = 5f;

    private void OnEnable()
    {
        playerDeadSequence += Die;
    }


    //string[] combatMove = { "Punch", "PunchToElbow" }
    Dictionary<int, KeyValuePair<string, float>>
          combatMove = new Dictionary<int, KeyValuePair<string, float>>();
    void Awake()
    {
        health = MAX_HEALTH;
        playerHealthSlider.value = health;
        playerIsHitting = false;
        Moves();
    }

    private void Moves()
    {
        combatMove.Add(0, new KeyValuePair<string, float>("Punch", 5f));
        combatMove.Add(1, new KeyValuePair<string, float>("EightPunch", 12.4f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {

            while (Enemy.enemyIsHitting) ;
            playerIsHitting = true;
            int move = ChooseRandomMove();
            string moveName = combatMove[move].Key;
            playerIsHitting = true;
            animator.SetTrigger(moveName);
            playerIsHitting = false;
        }
    }

    private int ChooseRandomMove()
    {
        return UnityEngine.Random.Range(0, combatMove.Count);
    }

    //triggered from animation
    public void EnemyDamage(float moveDamage)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, punchRaycastDistance))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.gameObject.GetComponent<Enemy>();
                enemy.Damage(moveDamage);
                enemy.showHitReaction();
            }
        }
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
#if DEBUG
        Debug.Log("Player health is reduced");
#endif
    }
    private void Die()
    {
        animator.SetTrigger("Dead");
    }
    private void OnDisable()
    {
        playerDeadSequence -= Die;
    }
}
