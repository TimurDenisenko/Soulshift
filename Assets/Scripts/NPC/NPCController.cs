using Assets.Scripts.NPC;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NPCType npcType = NPCType.Friendly;
    public NPCClass npcClass = NPCClass.Ranged;

    [Header("General settings")]
    public Transform player;
    public float minDistance = 10f;
    public float chaseDistance = 20f;

    [Header("Patrol settings")]
    public Transform[] waypoints;
    public float transitionDelay = 0;

    private NavMeshAgent agent;
    private Animator animator;
    private NPCAnimationEvent animationEvent;
    private int currentWaypoint = 0;
    private bool isWaiting;
    private string currentAnimation = "Idle";
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animationEvent = GetComponent<NPCAnimationEvent>();
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
            animationEvent.EndSingleMagicAttack();
            ChangeSpeed(5);
            agent.destination = player.position;
        }
        else if (distanceToPlayer <= minDistance)
        {
            agent.destination = transform.position;
            transform.LookAt(player.position);
            StartAttack();
        }
        else
        {
            Patrol();
        }
    }


    void Patrol()
    {
        ChangeSpeed(3);
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

    void StartAttack()
    {
        switch (npcClass)
        {
            case NPCClass.Melee:
                break;
            case NPCClass.Ranged:
                RangeAttack();
                break;
            case NPCClass.Civilian:
                break;
            case NPCClass.QuestGiver:
                break;
            case NPCClass.Summoner:
                break;
            default:
                break;
        }
    }

    void RangeAttack()
    {
        if (currentAnimation == "SingleMagicAttack")
            return;
        currentAnimation = "SingleMagicAttack";
        animator.PlayAnimation(currentAnimation);
    }

    void ChangeSpeed(float speed)
    {
        string newAnimation = speed switch
        {
            3 => "WalkForward",
            5 => "BattleRunForward",
            _ => "Idle"
        };
        if (currentAnimation == newAnimation)
            return;
        currentAnimation = newAnimation;
        agent.speed = speed;
        animator.SetFloat("speed", agent.velocity.magnitude);
        if (speed == 0)
        {
            animator.StopAnimation();
            return;
        }
        animator.PlayAnimation(currentAnimation);
    }
}
  