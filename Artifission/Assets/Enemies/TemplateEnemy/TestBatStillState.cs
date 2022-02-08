using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBatStillState : MonoBehaviour, ICreatureState
{
    public float attackDistance;
    public MonoBehaviour outState_Charge;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if((context.playerReference.transform.position - context.creatureReference.transform.position).magnitude < attackDistance || messages.Contains(StateMessages.Hit))
        {
            context.brainReference.ChangeState(outState_Charge as ICreatureState);
        }
    }

    public void InitateState(StateContext context)
    {
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
