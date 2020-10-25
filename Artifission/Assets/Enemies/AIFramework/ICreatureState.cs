using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatureState
{
    void ExecuteState(StateContext context);
    bool QueryValidity(StateContext context, StateMessages message = StateMessages.None);
    void InitateState(StateContext context);
}
