using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateAnimationScript : MonoBehaviour
{
    [SerializeField]
    Player player;
    [SerializeField]
    Enemy enemy;
    [SerializeField]
    GameObject fireball;
    [SerializeField]
    Transform fireballPoint;

    [SerializeField]
    AudioManager audioManager;

    public void DamageEnemy(float damage)
    {
        //if (gameObject.CompareTag("Player"))
        {
            player.EnemyDamage(damage);
        }

    }
    public void HealEnemy(float health)
    {
        enemy.Heal(health);
    }
    public void DamagePlayer(float damage)
    {

        enemy.PlayerDamage(damage);
        Debug.Log("We damage player");

    }
    public void EnemyFireBall(float damage)
    {
        fireball.transform.position = fireballPoint.position;
        fireball.transform.LookAt(player.transform.position);
        fireball.SetActive(true);
        enemy.PlayerDamage(damage);
    }
}
