using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Soldiers : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player, enemy;
    public LayerMask whatIsEnemy, WhatIsGround;

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public bool isAttacking = false;

    public float sightRange, attackRange;
    public bool EnemyInSightRange, EnemyInAttackRange;

    public Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        FindClosestEnemy();

        EnemyInSightRange = enemy != null && Vector3.Distance(transform.position, enemy.position) <= sightRange;
        EnemyInAttackRange = enemy != null && Vector3.Distance(transform.position, enemy.position) <= attackRange;

        if (!EnemyInSightRange && !EnemyInAttackRange)
        {
            ChasePlayer();
        }
        else if (EnemyInSightRange && !EnemyInAttackRange)
        {
            ChaseEnemy();
        }
        else if (EnemyInSightRange && EnemyInAttackRange)
        {
            Attack();
        }

        if (animator.GetBool("Died") == true)
        {
            animator.SetBool("Running", false);
            animator.SetBool("Attack", false);
        }

        FreezeRotation();
    }

    private void ChasePlayer()
    {
        if (animator.GetBool("Died") == true) return;

        gameObject.GetComponent<NavMeshAgent>().speed = 5;
        agent.SetDestination(player.position);
        animator.SetBool("Running", true);
        animator.SetBool("Attack", false);
        if ((transform.position - player.position).magnitude < 5f)
        {
            animator.SetBool("Running", false);
            agent.SetDestination(transform.position);
        }
    }

    private void ChaseEnemy()
    {
        if (enemy == null || animator.GetBool("Died") == true) return;

        gameObject.GetComponent<NavMeshAgent>().speed = 5;
        agent.SetDestination(enemy.position);
        animator.SetBool("Running", true);
        animator.SetBool("Attack", false);
    }

    private void Attack()
    {
        if (enemy == null || animator.GetBool("Died") == true) return;

        agent.SetDestination(transform.position);
        animator.SetBool("Running", false);
        LookAtTarget(enemy);

        if (!alreadyAttacked)
        {
            isAttacking = true;
            animator.SetBool("Attack", true);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        isAttacking = false;
        animator.SetBool("Attack", false);
    }

    private void FindClosestEnemy()
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, sightRange, whatIsEnemy);
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider enemyCollider in enemiesInRange)
        {
            if (Physics.CheckSphere(enemyCollider.transform.position, 0.1f, whatIsEnemy))
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemyCollider.transform.position);
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = enemyCollider.transform;
                }
            }
        }

        enemy = closestEnemy;
    }

    private void LookAtTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; // Keep only the horizontal direction
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Adjust the rotation speed as needed
        }
    }

    private void FreezeRotation()
    {
        Quaternion rotation = transform.rotation;
        rotation.x = 0;
        rotation.z = 0;
        transform.rotation = rotation;
    }
}
