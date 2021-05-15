using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollIdleState : MonoBehaviour, ICreatureState
{
    public float detectionRange;
    public MonoBehaviour outState_Alert;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if ((transform.position - context.playerReference.transform.position).magnitude < detectionRange || messages.Contains(StateMessages.Hit) || messages.Contains(StateMessages.PlayerDetected))
        {
            context.clusterReference.PropogateMessage(StateMessages.PlayerDetected);
            context.brainReference.ChangeState(outState_Alert as ICreatureState);
        }
    }

    public void InitateState(StateContext context)
    {
    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}
