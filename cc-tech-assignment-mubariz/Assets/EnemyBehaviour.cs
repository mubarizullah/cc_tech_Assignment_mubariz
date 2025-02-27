using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float exitRadius = 15f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 3f;
    [SerializeField] private GameObject wanderingBehavior;

    private NavMeshAgent agent;
    private bool isChasing = false;
    private bool isAttacking = false;

    public static event Action OnPlayerGetsDamage;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRange;
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
            if (distanceToPlayer > attackRange + 0.5f)
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
        ResumeChasing(); 
    }

    public void StopChasing()
    {
        isChasing = false;
        isAttacking = false;
        wanderingBehavior.SetActive(true);
        StopAllCoroutines(); 
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
            Debug.Log("Attacked Player");
            OnPlayerGetsDamage?.Invoke();
            yield return new WaitForSeconds(attackCooldown);

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
