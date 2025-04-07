using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject baseObj = GameObject.FindGameObjectWithTag("Base");
        if (baseObj != null)
        {
            target = baseObj.transform;
            agent.SetDestination(target.position);
        }
    }
}
