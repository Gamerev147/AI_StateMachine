using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class AI_Agent : MonoBehaviour
{
    public AI_StateMachine StateMachine;
    public StateID initialState;

    [Header("AI Agent Properties")]
    public bool enableChase = true;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    [Header("AI Field of View")]
    [Range(0f, 180f)] public float fieldOfViewAngle = 90f;

    [Space(10), Header("AI State Properties")]
    [Header("Chase Properties")]
    public float chaseUpdateTime;
    public float chaseDistance;
    public float chaseWalkDistance;

    [Header("Patrol Properties")]
    public Transform[] patrolWaypoints;
    public float patrolTimer = 14f;
    public float alertTime = 4f;

    [Header("Attack Properties")]
    public float attackCooldown = 2f;
    public float attackRange = 1.75f;
    [HideInInspector] public float attackTimer = 0f;

    [Header("References")]
    public ThirdPersonController playerController;
    [SerializeField] private ParticleSystem backstabBlood;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public AI_Weapon weapon;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        weapon = GetComponentInChildren<AI_Weapon>();

        //Register state machine and all used states
        StateMachine = new AI_StateMachine(this);
        StateMachine.RegisterState(new AI_StateIdle());
        StateMachine.RegisterState(new AI_StateChase());
        StateMachine.RegisterState(new AI_StateAttack());
        StateMachine.RegisterState(new AI_StateDead());
        StateMachine.ChangeState(initialState);
    }

    private void Update()
    {
        //Update state machine and animator
        StateMachine.Update();
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    public void EnableWeaponCollider()
    {
        weapon.ActivateCollider();
    }

    public void DisableWeaponCollider()
    {
        weapon.DeactivateCollider();
    }

    public void EnableRootMotion()
    {
        animator.applyRootMotion = true;
    }

    public void DisableRootMotion()
    {
        animator.applyRootMotion = false;
    }

    public void OnAnimationAudio(AudioClip audio)
    {
        AudioSource.PlayClipAtPoint(audio, transform.position);
    }

    public void PlayBackstabAnimation()
    {
        animator.SetTrigger("Stabbed");
    }

    public void PlayBackstabBlood()
    {
        backstabBlood.Play();
    }

    public bool CanSeePlayer()
    {
        Vector3 directionToPlayer = playerTransform.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.forward);

        if (angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0f, 0.5f, 0f), directionToPlayer.normalized, out hit, chaseDistance))
            {
                if (hit.collider.transform == playerTransform)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Gizmos.color = Color.yellow;
        Vector3 fovLine1 = Quaternion.AngleAxis(fieldOfViewAngle * 0.5f, transform.up) * transform.forward * chaseDistance;
        Vector3 fovLine2 = Quaternion.AngleAxis(-fieldOfViewAngle * 0.5f, transform.up) * transform.forward * chaseDistance;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }
}
