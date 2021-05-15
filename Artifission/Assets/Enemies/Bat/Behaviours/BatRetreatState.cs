using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatRetreatState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Flock;
    public float flightSpeed;
    public float retreatTime;
    public float timeVariance;

    private Rigidbody2D thisRigidbody;
    private float retreatTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        thisRigidbody.AddForce(flightSpeed * (context.creatureReference.transform.position - context.playerReference.transform.position).normalized);

        if (retreatTimer <= 0)
        {
            context.brainReference.ChangeState(outState_Flock as ICreatureState);
        }

        if (messages.Contains(StateMessages.Retreat))
        {
            retreatTimer = retreatTime + Random.Range(-timeVariance, timeVariance);
        }

        retreatTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        retreatTimer = retreatTime + Random.Range(-timeVariance, timeVariance);
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
