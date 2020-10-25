using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public enum StateMessages
{
    None,
    Death,
    Stunned
}

public class CreatureBrainCore : MonoBehaviour
{
    public MonoBehaviour defaultState;
    public MonoBehaviour[] overrideActions;

    private StateContext contextObject;
    private ICreatureState mDefaultState;
    private ICreatureState[] mOverrideStates;
    private ICreatureState currentState;

    private void Start()
    {
        mDefaultState = defaultState as ICreatureState;
        mOverrideStates = overrideActions.Select(e => e as ICreatureState).ToArray();
        currentState = mDefaultState;
        contextObject = GetComponent<StateContext>();
    }

    private void Update()
    {
        contextObject.UpdateContext();
        currentState.ExecuteState(contextObject);

        foreach(ICreatureState state in mOverrideStates)
        {
            if(state.QueryValidity(contextObject))
            {
                ChangeState(state);
                break;
            }
        }
    }

    public void ChangeState(ICreatureState newState)
    {
        newState.InitateState(contextObject);
        currentState = newState;
    }

    public void SendMessage(StateMessages message)
    {
        contextObject.UpdateContext();

        foreach(ICreatureState state in mOverrideStates)
        {
            if(state.QueryValidity(contextObject, message))
            {
                ChangeState(state);
                break;
            }
        }
    }
}
