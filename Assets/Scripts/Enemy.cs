using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{

    public static event Action enemyDeadSequence;

    private float health;
    private float MAX_HEALTH = 100;
    private float MIN_HEALTH = 0;
    public static bool enemyIsHitting;

    Dictionary<int, KeyValuePair<string, float>>
          combatMove = new Dictionary<int, KeyValuePair<string, float>>();
    Dictionary<int, KeyValuePair<string, float>>
         hitReactions = new Dictionary<int, KeyValuePair<string, float>>();

    [SerializeField]
    Animator animator;

    [SerializeField]
    private Slider playerHealthSlider;

    [SerializeField]
    private float punchRaycastDistance = 2f;

    private float currentTime = 2f;
    private float maxTime = 2f;

    //AI
    [SerializeField]
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private float distanceToTarget = Mathf.Infinity;
    [SerializeField]
    private float maintainDistance = 1f;

    private void OnEnable()
    {
        enemyDeadSequence += Die;
    }
    void Awake()
    {
        health = MAX_HEALTH;
        playerHealthSlider.value = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = maintainDistance;
        enemyIsHitting = false;
        Moves();
        HitReaction();
    }

    private void Moves()
    {
        combatMove.Add(0, new KeyValuePair<string, float>("Enemy_Fireball", 5f));
        combatMove.Add(1, new KeyValuePair<string, float>("Enemy_Kicking", 12.4f));
        combatMove.Add(2, new KeyValuePair<string, float>("EnemyBattleCry", 12.4f));

    }
    private void HitReaction()
    {
        hitReactions.Add(0, new KeyValuePair<string, float>("Enemy_PunchHitReaction_00", 0f));
        hitReactions.Add(1, new KeyValuePair<string, float>("Enemy_PunchHitReaction_01", 0f));

    }
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        navMeshAgent.SetDestination(target.position);
        if (distanceToTarget >= maintainDistance)
        {
            animator.SetBool("isRunning", true);
            currentTime = 0f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            //while (Player.playerIsHitting) ;
            //if(play)

            if (currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                if (Player.playerIsHitting) return;
                enemyIsHitting = true;
                int move = ChooseRandomMove();
                Debug.Log("We choost Move" + move);
                string moveName = combatMove[move].Key;
                animator.SetTrigger(moveName);
                enemyIsHitting = false;
                currentTime = 0f;
            }

        }
    }
    public void showHitReaction()
    {
        int hitReactionIndex = ChooseRandomHitReaction();
        string hitReaction = hitReactions[hitReactionIndex].Key;
        animator.SetTrigger(hitReaction);
    }
    private int ChooseRandomHitReaction()
    {
        return UnityEngine.Random.Range(0, hitReactions.Count);
    }
    private int ChooseRandomMove()
    {
        return UnityEngine.Random.Range(0, combatMove.Count);
    }

    //triggered from animation
    public void PlayerDamage(float moveDamage)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, punchRaycastDistance))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<Player>().Damage(moveDamage);
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
    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(transform.position, maintainDistance);
    }
}
