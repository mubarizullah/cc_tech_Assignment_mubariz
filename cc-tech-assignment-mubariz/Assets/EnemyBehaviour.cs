using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] GameObject wanderingGameObject;

    private void Start()
    {
         agent = GetComponent<NavMeshAgent>();
    }


}
