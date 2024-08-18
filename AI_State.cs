using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateID
{
    Idle,
    Patrol,
    Chase,
    Attack,
    Dead
}

public interface AI_State
{
    StateID GetID();
    void Enter(AI_Agent agent);
    void Update(AI_Agent agent);
    void Exit(AI_Agent agent);
}
