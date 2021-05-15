using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordGoblinPositionState : MonoBehaviour, ICreatureState
{
    public float closeZone;
    public float farZone;
    public float movementForce;
    public MonoBehaviour outState_Charge;
    public float chargePollRate;
    public float chargeChance;
    public float chargeCooldown;
    public float maxRunSpeed;
    public float clusterRadius;
    public float antiClusterStrength;
    public float moveSpeedVariance;
    public LayerMask LOSIgnoreMask;

    private Rigidbody2D creatureRigidbody;
    private float chargeTimer;
    private float adjustedMoveSpeed;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        float distanceFromPlayer = (transform.position - context.playerReference.transform.position).magnitude;
        Move(context, distanceFromPlayer);
        ApplyAntiClusterForce(context);
        EvaluateForCharge(context, distanceFromPlayer);
        chargeTimer -= Time.deltaTime;
    }

    private void ApplyAntiClusterForce(StateContext context)
    {
        foreach (GameObject alliedCreature in context.clusterReference.GetAllLivingCreatures().Where(e => e != context.creatureReference && Vector2.Distance(transform.position, e.transform.position) < clusterRadius))
        {
            creatureRigidbody.AddForce((transform.position - alliedCreature.transform.position).normalized * antiClusterStrength);
        }
    }

    private void Move(StateContext context, float distanceFromPlayer)
    {
        Vector2 movementVector = (transform.position - context.playerReference.transform.position);
        movementVector.y = 0;
        movementVector.Normalize();

        if (distanceFromPlayer >= farZone)
        {
            movementVector *= -1;
        }
        else if (distanceFromPlayer >= closeZone)
        {
            movementVector *= 0;
        }

        creatureRigidbody.AddForce(movementVector * movementForce);

        if (Mathf.Abs(creatureRigidbody.velocity.x) > adjustedMoveSpeed)
        {
            creatureRigidbody.velocity = creatureRigidbody.velocity.normalized * adjustedMoveSpeed;
        }
    }

    private void EvaluateForCharge(StateContext context, float distanceFromPlayer)
    {
        if (chargeTimer <= 0)
        {
            chargeTimer = chargePollRate;

            RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, (context.playerReference.transform.position - transform.position).normalized, distanceFromPlayer, ~LOSIgnoreMask);

            if (Random.value < chargeChance && lineOfSight && lineOfSight.collider.tag == "Player")
            {
                context.creatureAnimator.SetTrigger("ChargeTrigger");
                context.brainReference.ChangeState(outState_Charge as ICreatureState);
            }
        }
    }

    public void InitateState(StateContext context)
    {
        creatureRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        chargeTimer = chargeCooldown;
        adjustedMoveSpeed = maxRunSpeed + Random.Range(-moveSpeedVariance, moveSpeedVariance);
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
