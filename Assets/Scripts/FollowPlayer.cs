using UnityEngine;
using UnityEngine.AI;
public class FollowPlayer : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform movePos;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        navMeshAgent.destination = movePos.position;
        
    }
}
