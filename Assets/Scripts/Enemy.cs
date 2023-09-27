using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
    AudioManager audioManager;


    [SerializeField]
    private Slider playerHealthSlider;

    [SerializeField]
    private float punchRaycastDistance = 2f;

    private float currentTime = 0;
    private float maxTime = 3f;
    [SerializeField]
    private Transform fireballPoint;
    //AI
    [SerializeField]
    private Transform target;
    private NavMeshAgent navMeshAgent;
    private float distanceToTarget = Mathf.Infinity;
    [SerializeField]
    private float maintainDistance = 1f;
    private bool isRunning;



    private void OnEnable()
    {
        enemyDeadSequence += Die;
    }
    void Start()
    {
        health = MAX_HEALTH;
        playerHealthSlider.value = health;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.stoppingDistance = maintainDistance;
        enemyIsHitting = false;
        isRunning = false;
        currentTime = maxTime;
        Moves();
        HitReaction();
    }

    private void Moves()
    {
        combatMove.Add(0, new KeyValuePair<string, float>("Enemy_Fireball", 5f));
        combatMove.Add(1, new KeyValuePair<string, float>("Enemy_Kicking", 12.4f));
        combatMove.Add(2, new KeyValuePair<string, float>("EnemyBattleCry", 12.4f));
        combatMove.Add(3, new KeyValuePair<string, float>("Enemy_Punch_L", 12.4f));
        combatMove.Add(4, new KeyValuePair<string, float>("Enemy_Punch_R", 12.4f));
        combatMove.Add(5, new KeyValuePair<string, float>("Enemy_Hook_L", 12.4f));
        combatMove.Add(6, new KeyValuePair<string, float>("Enemy_Hook_R", 12.4f));
    }
    private void HitReaction()
    {
        hitReactions.Add(0, new KeyValuePair<string, float>("Enemy_PunchHitReaction_00", 0f));
        hitReactions.Add(1, new KeyValuePair<string, float>("Enemy_PunchHitReaction_01", 0f));

    }
    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget >= maintainDistance)
        {
            navMeshAgent.SetDestination(target.position);
            if (!isRunning && GameManager.floatingSequenceHasBeenPlayed)
            {
                animator.SetBool("isRunning", true);
                isRunning = true;

            }
            currentTime = 0f;
        }
        else
        {
            animator.SetBool("isRunning", false);
            //while (Player.playerIsHitting) ;
            //if(play)
            isRunning = false;
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
                if (moveName == "EnemyBattleCry")
                {
                    audioManager.PlayRageSound();
                }
                else if (moveName == "Enemy_Fireball")
                {
                    Invoke("PlayFireBallSound", 2f);
                }
                else
                {
                    Invoke("PlayPunchSound", 1f);
                }
                animator.SetTrigger(moveName);
                enemyIsHitting = false;
                currentTime = 0f;
            }

        }
    }
    private void PlayFireBallSound()
    {
        audioManager.PlayFireballSound();
    }
    private void PlayPunchSound()
    {
        audioManager.PlayPunchSound();
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
        //Debug.Log("We hit something");
        //Debug.DrawRay(transform.position, transform.forward);
        float offset = fireballPoint.position.y;
        if (Physics.Raycast(transform.position + new Vector3(0, offset, 0), transform.forward, out hit, punchRaycastDistance))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                audioManager.PlayGetHitSound();

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
        SceneManager.LoadScene(1);
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
