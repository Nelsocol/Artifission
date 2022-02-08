using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearGoblinIdleState : MonoBehaviour, ICreatureState
{
    public float detectionRange;
    public MonoBehaviour outState_Position;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if ((transform.position - context.playerReference.transform.position).magnitude < detectionRange || messages.Contains(StateMessages.Hit) || messages.Contains(StateMessages.PlayerDetected))
        {
            context.clusterReference.PropogateMessage(StateMessages.PlayerDetected);
            context.brainReference.ChangeState(outState_Position as ICreatureState);
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
