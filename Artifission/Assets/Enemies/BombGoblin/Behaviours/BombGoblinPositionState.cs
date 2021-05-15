using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGoblinPositionState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Throw;
    public MonoBehaviour outState_Spray;
    public MonoBehaviour outState_Jump;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {

    }

    public void InitateState(StateContext context)
    {

    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
