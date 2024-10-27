using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DM;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour, IDamagable
{
    public int maxHealth = 5;
    public int currentHealth;
    [SerializeField] FloatingHealthBar healthBar;
    public AnimationClip attackAnimation;
    public AnimationClip deathAnimation;
    public NavMeshAgent agent;
    public Transform player, tower, soldier;
    public Transform[] walls;
    public LayerMask whatIsPlayer, WhatIsGround, whatIsSoldier;
    public GameObject[] coins,soldiers;
    GameObject coin;

    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, soldierInSightRange, soldierInAttackRange;

    public Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        for (int i = 0; i < GameObject.Find("Castle").transform.childCount; i++)
        {
            walls[i] = GameObject.Find("Castle").transform.GetChild(i);
        }
        agent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        soldierInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsSoldier);
        soldierInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsSoldier);

        soldiers = GameObject.FindGameObjectsWithTag("soldier");

        if (playerInSightRange)
        {
            if (playerInAttackRange) AttackPlayer();
            else ChasePlayer();
        }
        else if (soldierInSightRange)
        {
            if (soldierInAttackRange) AttackSoldier();
            else ChaseSoldier();
        }
        else
        {
            MoveToTower();
        }

        if (animator.GetBool("Died") == true)
        {
            animator.SetBool("Running", false);
            animator.SetBool("Walking", false);
            animator.SetBool("Attack", false);
        }
    }

    private void MoveToTower()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            if (tower == null) tower = walls[0];
            else if ((transform.position - walls[i].position).magnitude > (transform.position - tower.position).magnitude) continue;
            else tower = walls[i];
        }

        if (soldier != null)
        {
            for (int i = 0; i < GameObject.Find("Castle").transform.childCount; i++)
            {
                walls[i] = GameObject.Find("Castle").transform.GetChild(i);
            }
        }

        gameObject.GetComponent<NavMeshAgent>().speed = 3;
        animator.SetBool("Running", false);
        animator.SetBool("Walking", true);
        agent.SetDestination(tower.position);
        if ((transform.position - tower.position).magnitude < 5f)
        {
            animator.SetBool("Walking", false);
            AttackTower();
        }
    }

    private void ChasePlayer()
    {
        gameObject.GetComponent<NavMeshAgent>().speed = 5;
        agent.SetDestination(player.position);
        animator.SetBool("Walking", false);
        animator.SetBool("Running", true);
    }

    private void ChaseSoldier()
    {

        for (int i = 0; i < soldiers.Length; i++)
        {
            if (soldier == null) soldier = soldiers[0].transform;
            else if ((transform.position - soldiers[i].transform.position).magnitude > (transform.position - soldier.position).magnitude) continue;
            else soldier = soldiers[i].transform;
        }

        gameObject.GetComponent<NavMeshAgent>().speed = 5;
        agent.SetDestination(soldier.position);
        animator.SetBool("Walking", false);
        animator.SetBool("Running", true);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("Running", false);
        LookAtTarget(player);

        if (!alreadyAttacked)
        {
            animator.SetBool("Attack", true);
            alreadyAttacked = true;
            StartCoroutine(EnemyAttack());
        }
    }

    private void AttackTower()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("Running", false);
        LookAtTarget(tower);

        if (!alreadyAttacked)
        {
            animator.SetBool("Attack", true);
            alreadyAttacked = true;
            StartCoroutine(EnemyAttack());
        }
    }

    private void AttackSoldier()
    {
        agent.SetDestination(transform.position);
        animator.SetBool("Running", false);
        LookAtTarget(soldier);

        if (!alreadyAttacked)
        {
            animator.SetBool("Attack", true);
            alreadyAttacked = true;
            StartCoroutine(EnemyAttack());
        }
    }

    IEnumerator EnemyAttack()
    {
        yield return new WaitForSeconds(attackAnimation.length);
        animator.SetBool("Attack", false);
        alreadyAttacked = false;
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            ControlManager.Instance.TakeDamage(1);
        }
    }

    IEnumerator Die()
    {
        agent.SetDestination(transform.position);
        Destroy(this.GetComponent<Collider>());
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(deathAnimation.length);
        Destroy(this.gameObject);
        int coinRandomizer = Random.Range(1, 10);
        if (coinRandomizer <= 5) { coin = coins[0]; }
        else if (coinRandomizer <= 8) { coin = coins[1]; }
        else coin = coins[2];
        Instantiate(coin, transform.position, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private void FindClosestEnemy()
    {
        Collider[] soldiersInRange = Physics.OverlapSphere(transform.position, sightRange, whatIsSoldier);
        float closestDistance = Mathf.Infinity;
        Transform closestSoldier = null;

        foreach (Collider soldierCollider in soldiersInRange)
        {
            if (Physics.CheckSphere(soldierCollider.transform.position, 0.1f, whatIsSoldier))
            {
                float distanceToSoldier = Vector3.Distance(transform.position, soldier.transform.position);
                if (distanceToSoldier < closestDistance)
                {
                    closestDistance = distanceToSoldier;
                    closestSoldier = soldierCollider.transform;
                }
            }
        }

        soldier = closestSoldier;
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
}

