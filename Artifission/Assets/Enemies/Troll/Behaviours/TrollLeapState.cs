using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollLeapState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Alert;
    public float leapForce;
    public float arcConstant;
    public LayerMask groundedLayers;

    private Rigidbody2D thisRigidbody;
    private Collider2D thisCollider;
    private bool grounded;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (!grounded && thisRigidbody.velocity.y == 0 && thisCollider.IsTouchingLayers(groundedLayers))
        {
            grounded = true;
        }

        if(grounded)
        {
            context.brainReference.ChangeState(outState_Alert as ICreatureState);
        }
    }

    public void InitateState(StateContext context)
    {
        float leapScalar = Vector2.Distance(context.creatureReference.transform.position, context.playerReference.transform.position);
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
        thisRigidbody.AddForce((context.playerReference.transform.position - context.creatureReference.transform.position + new Vector3(0, arcConstant)) * leapForce);
        grounded = false;
        thisCollider = context.creatureReference.GetComponent<Collider2D>();
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
