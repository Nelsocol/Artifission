using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttackState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Retreat;
    public float flightSpeed;    
    public float retreatChanceOnHit;
    public float flightSpeedVariance;
    public float attackTime;
    public float pauseTime;

    private Rigidbody2D thisRigidbody;
    private Collider2D thisCollider;
    private Collider2D playerCollider;
    private float adjustedFlightSpeed;
    private float attackTimer;
    private float pauseTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (pauseTimer <= 0)
        {
            thisRigidbody.AddForce(adjustedFlightSpeed * (context.playerReference.transform.position - context.creatureReference.transform.position).normalized);
            if (messages.Contains(StateMessages.HitPlayer))
            {
                context.brainReference.ChangeState(outState_Retreat as ICreatureState);

                if (Random.value < retreatChanceOnHit)
                {
                    context.clusterReference.PropogateMessage(StateMessages.Retreat);
                }
            }

            if (messages.Contains(StateMessages.Retreat) || attackTimer <= 0)
            {
                context.brainReference.ChangeState(outState_Retreat as ICreatureState);
            }
            attackTimer -= Time.deltaTime;
        }
        pauseTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        thisCollider = context.creatureReference.GetComponent<Collider2D>();
        playerCollider = context.playerReference.GetComponent<Collider2D>();
        adjustedFlightSpeed = flightSpeed + Random.Range(-flightSpeedVariance, flightSpeedVariance);
        attackTimer = attackTime;
        pauseTimer = pauseTime;
        thisRigidbody.velocity = Vector2.zero;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
