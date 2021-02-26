using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : Enemy
{
    [Range(0.75f, 4f)]
    public float attackRate = 1.75f;

    float nextAttackTime = 0;

    void OnAttack()
    {
        if (nextAttackTime < Time.time)
        {
            Debug.Log("On-Attack");

            nextAttackTime = Time.time + attackRate;
        }
    }

    void OnWarning()
    {
        if (hp < 15)
        {
            ChasePlayer(2);
        } else {
            ChasePlayer();
        }
    }

    void OnIdle()
    {
        GoRandomPoint(20);
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
        Debug.Log("Orc die");
    }
}