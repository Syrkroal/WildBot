using UnityEngine;
using UnityEngine.AI;
public class SwarmGuy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    public void SetTargetPosition(Vector3 swarmCenterAverage, Vector3 swarmMovementAverage)
    {
        Vector3 destination = swarmCenterAverage + swarmMovementAverage;
        navMeshAgent.SetDestination(destination);
    }

    public void SetPlayer(Vector3 posPlayer)
    {
        print("setPlayer");
        navMeshAgent.SetDestination(posPlayer);
    }
}