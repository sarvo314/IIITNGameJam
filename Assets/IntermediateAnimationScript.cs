using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermediateAnimationScript : MonoBehaviour
{
    [SerializeField]
    Player player;
    public void MoveDamage(float damage)
    {
        player.MoveDamage(damage);
    }
}
