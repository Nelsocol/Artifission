using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpearGoblinPositionState : MonoBehaviour, ICreatureState
{
    public float closeZone;
    public float farZone;
    public float movementForce;
    public MonoBehaviour outState_Strike;
    public MonoBehaviour outState_Throw;
    public float throwPollRate;
    public float throwChance;
    public float throwCooldown;
    public float maxRunSpeed;
    public float strikeRange;
    public float clusterRadius;
    public float antiClusterStrength;
    public float moveSpeedVariance;
    public LayerMask LOSIgnoreLayers;

    private Rigidbody2D creatureRigidbody;
    private float throwTimer;
    private float adjustedMoveSpeed;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        float distanceFromPlayer = (transform.position - context.playerReference.transform.position).magnitude;
        Move(context, distanceFromPlayer);
        ApplyAntiClusterForce(context);
        EvaluateForThrow(context, distanceFromPlayer);
        EvaluateForStrike(context, distanceFromPlayer);
        throwTimer -= Time.deltaTime;
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

    private void EvaluateForStrike(StateContext context, float distanceFromPlayer)
    {
        if (distanceFromPlayer < strikeRange)
        {
            context.brainReference.ChangeState(outState_Strike as ICreatureState);
        }
    }

    private void EvaluateForThrow(StateContext context, float distanceFromPlayer)
    {
        if (throwTimer <= 0)
        {
            throwTimer = throwPollRate;

            RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, (context.playerReference.transform.position - transform.position).normalized, distanceFromPlayer, ~LOSIgnoreLayers);

            if (Random.value < throwChance && lineOfSight && lineOfSight.collider.tag == "Player")
            {
                context.creatureAnimator.SetTrigger("ThrowTrigger");
                context.brainReference.ChangeState(outState_Throw as ICreatureState);
            }
        }
    }

    public void InitateState(StateContext context)
    {
        creatureRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        throwTimer = throwCooldown;
        adjustedMoveSpeed = maxRunSpeed + Random.Range(-moveSpeedVariance, moveSpeedVariance);
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
