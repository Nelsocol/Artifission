using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBatStunnedState : MonoBehaviour, ICreatureState
{
    public float stunTime;
    public MonoBehaviour outState_Charge;

    private Rigidbody2D creatureRigidbody;
    private float elapsedTime;

    public void ExecuteState(StateContext context)
    {
        if(elapsedTime > stunTime)
        {
            creatureRigidbody.gravityScale = 0;
            context.brainReference.ChangeState(outState_Charge as ICreatureState);
        }
        elapsedTime += Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        creatureRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        elapsedTime = 0;
        creatureRigidbody.gravityScale = 2;
    }

    public bool QueryValidity(StateContext context, StateMessages message = StateMessages.None)
    {
        return message == StateMessages.Stunned;
    }
}
