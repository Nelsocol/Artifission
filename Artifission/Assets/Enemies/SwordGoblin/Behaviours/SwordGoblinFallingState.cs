using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordGoblinFallingState : MonoBehaviour, ICreatureState
{
    public float velocityLimit;
    public MonoBehaviour outState_Stunned;

    private Rigidbody2D thisRigidbody; 

    private void Start()
    {
        thisRigidbody = GetComponentInParent<Rigidbody2D>();
    }

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (thisRigidbody.velocity.y == 0)
        {
            context.brainReference.ChangeState(outState_Stunned as ICreatureState);
        }
    }

    public void InitateState(StateContext context)
    { 
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return Mathf.Abs(thisRigidbody.velocity.y) > velocityLimit;
    }
}
