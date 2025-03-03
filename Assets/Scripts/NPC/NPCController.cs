using Assets.Scripts.NPC;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NPCType npcType = NPCType.Friendly;

    [Header("General settings")]
    public Transform player;
    public float minDistance = 10f;
    public float chaseDistance = 20f; 

    [Header("Patrol settings")]
    public Transform[] waypoints;
    public float transitionDelay = 0;

    private NavMeshAgent agent;
    private Animator animator;
    private int currentWaypoint = 0;
    private bool isWaiting;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (npcType)
        {
            case NPCType.Friendly:
                FriendlyBehavior();
                break;

            case NPCType.Neutral:
                NeutralBehavior();
                break;

            case NPCType.Aggressive:
                AggressiveBehavior();
                break;
        }
        animator.SetFloat("speed", agent.velocity.magnitude);
        animator.Update(0);
    }

    void LateUpdate()
    {
    }

    void FriendlyBehavior()
    {
        if (Vector3.Distance(transform.position, player.position) < chaseDistance)
        {
        }
        else
        {
            Patrol();
        }
    }

    void NeutralBehavior()
    {
        Patrol();
    }

    void AggressiveBehavior()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance && distanceToPlayer > minDistance)
        {
            agent.destination = player.position;
        }
        else if (distanceToPlayer <= minDistance)
        {
            agent.destination = transform.position;
        }
        else
        {
            Patrol();
        }
    }


    void Patrol()
    {
        if (waypoints.Length == 0)
            return;
        if (isWaiting)
        {
            Invoke(nameof(DisableWaiting), transitionDelay);
            return;
        }
        agent.destination = waypoints[currentWaypoint].position; 
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            isWaiting = true;
        }
    }

    void DisableWaiting()
    {
        isWaiting = false;
    }
}
  