using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateIdle : AI_State
{
    public void Enter(AI_Agent agent)
    {
        agent.navMeshAgent.isStopped = true;
    }

    public void Exit(AI_Agent agent)
    {
        
    }

    public StateID GetID()
    {
        return StateID.Idle;
    }

    public void Update(AI_Agent agent)
    {
        //maybe do something to switch up the idle anims every so often...
        //..

        //Check to see if player is within field of view
        if (agent.CanSeePlayer() && agent.enableChase)
        {
            agent.StateMachine.ChangeState(StateID.Chase);
        }
    }
}
