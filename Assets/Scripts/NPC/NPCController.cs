using Assets.Scripts.NPC;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NPCType npcType = NPCType.Friendly;

    [Header("General settings")]
    public Transform player;
    public float chaseDistance = 10f; 

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
        if (Vector3.Distance(transform.position, player.position) < chaseDistance)
        {
            agent.destination = player.position;
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
        agent.destination = waypoints[currentWaypoint].position; 
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (isWaiting)
            {
                Invoke(nameof(DisableWaiting), transitionDelay);
                return;
            }
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            isWaiting = true;
        }
    }

    void DisableWaiting()
    {
        isWaiting = false;
    }
}
  