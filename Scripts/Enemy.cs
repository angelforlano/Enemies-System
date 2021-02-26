using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public List<Transform> patrol = new List<Transform>();
    
    [Range(5, 100)]
    public int hp = 30;

    [Range(1, 12)]
    public float warningRange = 6f;

    [Range(1, 12)]
    public float attackRange = 4f;

    protected abstract void Die();

    protected Player player;

    int patrolPointIndex;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, warningRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    protected bool PlayerOnRange(float range)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        
        foreach (var collider in hitColliders)
        {
            if(collider.gameObject.CompareTag("Player"))
            {
                player = collider.gameObject.GetComponent<Player>();
                return true;
            }
        }

        player = null;
        return false;
    }

    protected void ChasePlayer(float speed = 4)
    {
        agent.speed = speed;
        agent.SetDestination(player.transform.position);
    }

    protected void GoRandomPoint(float distance = 5)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
           agent.SetDestination(Random.insideUnitSphere * distance); 
        }
    }

    protected void GoPatrolPoint()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(patrol[patrolPointIndex].position);

            patrolPointIndex++;

            if(patrolPointIndex >= patrol.Count)
            {
                patrolPointIndex = 0;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;

        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }
}
