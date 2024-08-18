using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateAttack : AI_State
{
    public void Enter(AI_Agent agent)
    {
        //agent.navMeshAgent.isStopped = true;
    }

    public void Exit(AI_Agent agent)
    {

    }

    public StateID GetID()
    {
        return StateID.Attack;
    }

    public void Update(AI_Agent agent)
    {
        agent.attackTimer -= Time.deltaTime;

        //Check player distance
        if (Vector3.Distance(agent.transform.position, agent.playerTransform.position) <= agent.attackRange)
        {
            agent.navMeshAgent.destination = agent.playerTransform.position;

            if (agent.attackTimer <= 0f)
            {
                int randomAttack = Random.Range(0, 2);

                agent.EnableRootMotion();
                agent.animator.SetTrigger("Attack");
                agent.animator.SetInteger("AttackNumber", randomAttack);
                agent.attackTimer = agent.attackCooldown;
            }
        } else
        {
            agent.StateMachine.ChangeState(StateID.Chase);
        }
    }
}
