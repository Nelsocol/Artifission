using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGoblinStaggeredScript : MonoBehaviour, ICreatureState
{
    public float stunTime;
    public MonoBehaviour outState_Position;

    private float stunTimer;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if (stunTimer <= 0)
        {
            context.brainReference.ChangeState(outState_Position as ICreatureState);
        }
        stunTimer -= Time.deltaTime;
    }

    public void InitateState(StateContext context)
    {
        stunTimer = stunTime;
        context.creatureReference.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return messages.Contains(StateMessages.Staggered);
    }
}
