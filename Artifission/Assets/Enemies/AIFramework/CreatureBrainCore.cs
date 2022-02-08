using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public enum StateMessages
{
    None,
    Death,
    Stunned,
    Staggered,
    Hit,
    PlayerDetected,
    HitPlayer,
    Retreat
}

public class CreatureBrainCore : MonoBehaviour
{
    public MonoBehaviour defaultState;
    public MonoBehaviour[] overrideActions;

    private StateContext contextObject;
    private ICreatureState mDefaultState;
    private ICreatureState[] mOverrideStates;
    private ICreatureState currentState;
    private List<StateMessages> queuedMessages = new List<StateMessages>();

    private void Start()
    {
        mDefaultState = defaultState as ICreatureState;
        mOverrideStates = overrideActions.Select(e => e as ICreatureState).ToArray();
        currentState = mDefaultState;
        contextObject = GetComponent<StateContext>();
        mDefaultState.InitateState(contextObject);
    }

    private void Update()
    {
        contextObject.UpdateContext();
        currentState.ExecuteState(contextObject, queuedMessages);

        foreach(ICreatureState state in mOverrideStates)
        {
            if(state.QueryValidity(contextObject, queuedMessages))
            {
                ChangeState(state);
                break;
            }
        }

        queuedMessages = new List<StateMessages>();
    }

    public void ChangeState(ICreatureState newState)
    {
        newState.InitateState(contextObject);
        currentState = newState;
    }

    public void SendMessage(StateMessages message)
    {
        queuedMessages.Add(message);
    }
}
