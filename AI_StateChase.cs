using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateChase : AI_State
{
    public void Enter(AI_Agent agent)
    {
        agent.navMeshAgent.isStopped = false;
        agent.navMeshAgent.speed = 1.5f;
    }

    public void Exit(AI_Agent agent)
    {
        
    }

    public StateID GetID()
    {
        return StateID.Chase;
    }

    public void Update(AI_Agent agent)
    {
        agent.navMeshAgent.destination = agent.playerTransform.position;

        //Check distance for attack
        if (Vector3.Distance(agent.transform.position, agent.playerTransform.position) <= agent.attackRange)
        {
            agent.StateMachine.ChangeState(StateID.Attack);
        }
    }
}
