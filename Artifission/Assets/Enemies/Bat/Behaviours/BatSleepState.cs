using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSleepState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Flock;
    public float detectionRange;
    public MonoBehaviour flightBehaviours;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {
        if ((context.creatureReference.transform.position - context.playerReference.transform.position).magnitude < detectionRange || messages.Contains(StateMessages.Hit) || messages.Contains(StateMessages.PlayerDetected))
        {
            context.clusterReference.PropogateMessage(StateMessages.PlayerDetected);
            flightBehaviours.enabled = true;
            context.brainReference.ChangeState(outState_Flock as ICreatureState);
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
