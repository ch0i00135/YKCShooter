using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;

    void Start()
    {
        agent.SetDestination(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(agent.steeringTarget);
    }
}
