using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour
{
    [SerializeField] private float wanderRadius = 10f; // How far the enemy can roam
    [SerializeField] private float wanderTimer = 5f; // Time between movement decisions
    [SerializeField] private Transform enemyRoot; // Reference to the parent (main enemy)

    private NavMeshAgent agent;
    private float timer;

    private void Start()
    {
        agent = enemyRoot.GetComponent<NavMeshAgent>(); // Get NavMeshAgent from parent
        timer = wanderTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = GetRandomNavMeshPosition();
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    // Generates a random point within the wanderRadius on the NavMesh
    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += enemyRoot.position; // Offset from the enemy's position

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
        {
            return hit.position; // Return a valid position on the NavMesh
        }

        return enemyRoot.position; // If no valid point found, stay in place
    }
}
