using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBatStillState : MonoBehaviour, ICreatureState
{
    public float attackDistance;
    public MonoBehaviour outState_Charge;

    private float previousHealth;

    public void ExecuteState(StateContext context)
    {
        if((context.playerReference.transform.position - context.creatureReference.transform.position).magnitude < attackDistance || context.creatureBindings.GetCurrentHealth() < previousHealth)
        {
            context.brainReference.ChangeState(outState_Charge as ICreatureState);
        }
        previousHealth = context.creatureBindings.GetCurrentHealth();
    }

    public void InitateState(StateContext context)
    {
        previousHealth = context.creatureBindings.GetCurrentHealth();
    }

    public bool QueryValidity(StateContext context, StateMessages message = StateMessages.None)
    {
        return false;
    }
}
