using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICreatureState
{
    void ExecuteState(StateContext context, List<StateMessages> messages);
    bool QueryValidity(StateContext context, List<StateMessages> messages);
    void InitateState(StateContext context);
}
