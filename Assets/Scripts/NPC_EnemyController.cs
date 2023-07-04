using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    CHASE,
    ATTACK
}

public class NPC_EnemyController : MonoBehaviour
{
    private PlayerAnimations enemyAnim;
    private NavMeshAgent navAgent;

    private Transform playerTarget;

    public float move_Speed = 7f;
    public float  attackDistance = 1f; // how close to get to enemy in order to start fight
    public float chasePlayerAfterAttackDistance = 1f; // if after attack player runs, enemy will follow him

    private float waitBeforeAttackTime = 2f;
    private float attackTimer; // denote when should we attack

    private EnemyState enemy_State;

    public GameObject attackPoint;

    void Awake()
    {
        enemyAnim = GetComponent<PlayerAnimations>();
        navAgent = GetComponent<NavMeshAgent>();

        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
    }

    void Start()
    {
        enemy_State = EnemyState.CHASE;
        attackTimer = waitBeforeAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_State == EnemyState.CHASE)
        {
            ChasePlayer();
        }

        if(enemy_State == EnemyState.ATTACK)
        {
             AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(playerTarget.position);// player position will be npc's destination
        navAgent.speed = move_Speed;

        if(navAgent.velocity.sqrMagnitude == 0)
        {
            enemyAnim.WalkForward(false);
        }
        else
        {
            enemyAnim.WalkForward(true);
        }

        if(Vector3.Distance(transform.position, playerTarget.position) <= attackDistance)
        {
            enemy_State = EnemyState.ATTACK;
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero; // stopping NavmeshAgent
        navAgent.isStopped = true;

        enemyAnim.WalkForward(false);

        attackTimer += Time.deltaTime;

        if(attackTimer > waitBeforeAttackTime)
        {
            if(Random.Range(0, 2) > 0)
            {
                enemyAnim.Attack_1();
            }
            else
            {
                enemyAnim.Attack_2();
            }

            attackTimer = 0; // set timer to zero after first attack to attack again
        } // if we can attack

        if(Vector3.Distance(transform.position, playerTarget.position) > attackDistance) // if player goes away, chase and attack again
        {
            navAgent.isStopped = false; // we enable our enemy to move again
            enemy_State = EnemyState.CHASE;
        }
    }

     void ActivateAttackPoint()
    {
            attackPoint.SetActive(true);
    }

    void DeactivateAttackPoint()
    {
        if(attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }
}
