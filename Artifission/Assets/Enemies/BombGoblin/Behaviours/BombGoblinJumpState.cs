﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGoblinJumpState : MonoBehaviour, ICreatureState
{
    public MonoBehaviour outState_Spray;
    public MonoBehaviour outState_Position;

    public void ExecuteState(StateContext context, List<StateMessages> messages)
    {

    }

    public void InitateState(StateContext context)
    {

    }

    public bool QueryValidity(StateContext context, List<StateMessages> messages)
    {
        return false;
    }
}