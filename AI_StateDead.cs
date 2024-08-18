using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_StateDead : AI_State
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
        return StateID.Dead;
    }

    public void Update(AI_Agent agent)
    {
        
    }
}
