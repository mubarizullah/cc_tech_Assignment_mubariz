using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float exitRadius = 15f;
    [SerializeField] private float attackRange = 2f; // Increased for better accuracy
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private GameObject wanderingBehavior;

    private NavMeshAgent agent;
    private bool isChasing = false;
    private bool isAttacking = false;

    public static event Action OnPlayerGetsDamage;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange; // Ensure enemy stops at attack range
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (!isChasing && distanceToPlayer <= detectionRadius)
        {
            StartChasing();
        }
        else if (isChasing && distanceToPlayer >= exitRadius)
        {
            StopChasing();
        }

        if (isChasing)
        {
            if (distanceToPlayer > attackRange + 0.5f) // Added buffer to avoid jittering
            {
                ResumeChasing();
            }
            else if (!isAttacking)
            {
                StartAttacking();
            }
        }
    }

    private void StartChasing()
    {
        isChasing = true;
        wanderingBehavior.SetActive(false);
        ResumeChasing(); // Ensure the agent starts moving
    }

    private void StopChasing()
    {
        isChasing = false;
        isAttacking = false;
        wanderingBehavior.SetActive(true);
        StopAllCoroutines(); // Stop attacking
    }

    private void ResumeChasing()
    {
        isAttacking = false;
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    private void StartAttacking()
    {
        isAttacking = true;
        agent.isStopped = true; // Stop movement when attacking
        StartCoroutine(AttackPlayer());
    }

    private IEnumerator AttackPlayer()
    {
        while (isAttacking)
        {
            Debug.Log("Attacking Player!"); // Check if attack starts
            yield return new WaitForSeconds(attackCooldown);

            if (OnPlayerGetsDamage != null)
            {
                OnPlayerGetsDamage.Invoke(); // Call event for player damage
                Debug.Log("Player health should decrease"); // Ensure event is fired
            }

            // If player moved away during attack, resume chasing
            if (Vector3.Distance(transform.position, player.position) > attackRange + 0.5f)
            {
                Debug.Log("Player moved away, resuming chase...");
                ResumeChasing();
                yield break; // Stop attacking
            }
        }
    }
}
