using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollWalkState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Alert;
    public float moveForce;
    public float maxMoveSpeed;
    public float alertRange;

    private Rigidbody2D thisRigidbody;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        Vector2 moveVector = Vector2.left;
        if (context.playerReference.transform.position.x > context.creatureReference.transform.position.x)
        {
            moveVector = Vector2.right;
        }

        thisRigidbody.AddForce(moveVector * moveForce);
        thisRigidbody.velocity = new Vector2(Mathf.Clamp(thisRigidbody.velocity.x, -maxMoveSpeed, maxMoveSpeed), thisRigidbody.velocity.y);

        if (Vector2.Distance(context.playerReference.transform.position, context.creatureReference.transform.position) < alertRange)
        {
            context.brainReference.ChangeState(outState_Alert as ICreatureState);
        }
    }

    public void InitateState(StateContext context)
    {
        thisRigidbody = context.creatureReference.GetComponent<Rigidbody2D>();
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
