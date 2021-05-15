using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatFlockState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Retreat;
    public MonoBehaviour outState_Attack;
    public float flightSpeed;
    public float retreatChanceOnHit;
    public float flightSpeedVariance;
    public float flockHeight;
    public float attackChance;
    public float attackPollRate;

    private Rigidbody2D thisRigidbody;
    private float adjustedFlightSpeed;
    private float attackTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        thisRigidbody.AddForce(adjustedFlightSpeed * ((context.playerReference.transform.position - context.creatureReference.transform.position + new Vector3(0, flockHeight, 0)).normalized));

        if(attackTimer <= 0)
        {
            attackTimer = attackPollRate;
            if (Random.value < attackChance)
            {
                context.creatureAnimator.SetTrigger("AttackTrigger");
                context.brainReference.ChangeState(outState_Attack as ICreatureState);
            }
        }

        if (messages.Contains(StateMessages.Retreat))
        {
            context.brainReference.ChangeState(outState_Retreat as ICreatureState);
        }

        attackTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        adjustedFlightSpeed = flightSpeed + Random.Range(-flightSpeedVariance, flightSpeedVariance);
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
