using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    [Range(0.25f, 4f)]
    public float attackRate = 2f;

    [Range(5, 20)]
    public int maxDamage = 10;

    float nextAttackTime;

    void OnAttack()
    {
        agent.stoppingDistance = 2;

        if (nextAttackTime < Time.time)
        {
            player.TakeDamage(Random.Range(1, maxDamage+1));
            nextAttackTime = Time.time + attackRate;
        }
    }

    void OnWarning()
    {
        agent.stoppingDistance = 0;
        ChasePlayer();
    }

    void OnIdle()
    {
        agent.stoppingDistance = 0;
        GoPatrolPoint();
    }

    void FixedUpdate()
    {
        if (PlayerOnRange(attackRange))
        {
            OnAttack();
        } else if (PlayerOnRange(warningRange)) {
            OnWarning();
        } else {
            OnIdle();
        }
    }

    protected override void Die()
    {
        Debug.Log("Wizard die");
    }
}