using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public static float health;
    private float MAX_HEALTH = 100;
    private float MIN_HEALTH = 0;


    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float punchRaycast;

    string[] combatMove = { "Punch", "PunchToElbow", "EightPunch" };
    void Awake()
    {
        health = MAX_HEALTH;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            int move = Random.Range(0, combatMove.Length);
            animator.SetTrigger(combatMove[move]);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, punchRaycast))
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("We hit enemy");
                }
            }
        }
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
