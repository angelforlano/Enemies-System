using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Enemy enemy;
    public int hp;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            enemy.TakeDamage(10);
        }
    }

    void Die()
    {
        Debug.Log("Player Die");
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