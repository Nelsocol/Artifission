using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBatRetreatState : MonoBehaviour, ICreatureState
{
    private Rigidbody2D creatureRigidbody;

    public MonoBehaviour outState_Charge;
    public float flightSpeed;
    public float retreatTime;

    private float elapsedTime = 0;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (elapsedTime < retreatTime)
        {
            creatureRigidbody.AddForce((context.creatureReference.transform.position - context.playerReference.transform.position).normalized * flightSpeed);
        }
        else
        {
            context.brainReference.ChangeState(outState_Charge as ICreatureState);
        }
        elapsedTime += Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        creatureRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        elapsedTime = 0;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
