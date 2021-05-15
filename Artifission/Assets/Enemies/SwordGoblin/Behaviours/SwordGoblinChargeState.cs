using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordGoblinChargeState : MonoBehaviour, ICreatureState
{
    public float chargeTime;
    public float chargeSpeed;
    public float chargeForce;
    public MonoBehaviour outState_Position;
    public MonoBehaviour outState_Strike;
    public float strikeDistance;

    private float chargeTimer;
    private Rigidbody2D thisRigidbody;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (chargeTimer > 0)
        {
            Vector2 movementVector = (context.playerReference.transform.position - transform.position);
            movementVector.y = 0;
            thisRigidbody.AddForce(movementVector.normalized * chargeForce);

            thisRigidbody.velocity = new Vector2(Mathf.Clamp(thisRigidbody.velocity.x, -chargeSpeed, chargeSpeed), thisRigidbody.velocity.y);

            if (Vector2.Distance(context.playerReference.transform.position, transform.position) < strikeDistance)
            {
                context.creatureAnimator.SetTrigger("IdleTrigger");
                context.brainReference.ChangeState(outState_Strike as ICreatureState);
            }
        }
        else
        {
            context.creatureAnimator.SetTrigger("IdleTrigger");
            context.brainReference.ChangeState(outState_Position as ICreatureState);
        }
        chargeTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        chargeTimer = chargeTime;
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        context.creatureAnimator.SetTrigger("ChargeTrigger");
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
