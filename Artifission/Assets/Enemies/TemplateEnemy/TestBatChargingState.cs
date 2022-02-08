using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TestBatChargingState : MonoBehaviour, ICreatureState
{
    private Rigidbody2D creatureRigidbody;
    private Collider2D creatureCollider;
    private Collider2D playerCollider;

    public MonoBehaviour outState_Retreat;
    public float flightSpeed;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        creatureRigidbody.AddForce((context.playerReference.transform.position - context.creatureReference.transform.position).normalized * flightSpeed);
        if(creatureCollider.IsTouching(playerCollider))
        {
            context.brainReference.ChangeState(outState_Retreat as ICreatureState);
        }
    }

    public void InitateState(StateContext context)
    {
        creatureRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        creatureCollider = context.creatureReference.GetComponent<Collider2D>();
        playerCollider = context.playerReference.GetComponent<Collider2D>();
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
