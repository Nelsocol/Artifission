using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInteractionBindings : MonoBehaviour
{
    public List<Action<ISpellInteractionType, Vector2>> mInteractionEvents { get; set; } = new List<Action<ISpellInteractionType, Vector2>>();

    public void SubscribeInteractionEvent(Action<ISpellInteractionType, Vector2> interactionEvent)
    {
        mInteractionEvents.Add(interactionEvent);
    }

    public void RaiseInteractionEvent(ISpellInteractionType eventData, Vector2 eventSource)
    {
        foreach (Action<ISpellInteractionType, Vector2> action in mInteractionEvents)
        {
            action(eventData, eventSource);
        }
    }
}
