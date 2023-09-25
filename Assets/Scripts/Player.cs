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
    private float MAX_HEALTH = 100;
    private float MIN_HEALTH = 0;
    [SerializeField]
    private Slider playerHealthSlider;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float punchRaycast;

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
        combatMove.Add(0, new KeyValuePair<string, float>("Punch", 5f));
        combatMove.Add(1, new KeyValuePair<string, float>("EightPunch", 12.4f));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            int move = UnityEngine.Random.Range(0, combatMove.Count);
            string moveName = combatMove[move].Key;
            float moveDamage = combatMove[move].Value;
            animator.SetTrigger(moveName);
            //this.MoveDamage(moveDamage);
        }
    }
    //triggered from animation
    public void MoveDamage(float moveDamage)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, punchRaycast))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<Enemy>().Damage(moveDamage);
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
