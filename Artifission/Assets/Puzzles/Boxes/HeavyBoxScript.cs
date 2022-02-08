using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBoxScript : MonoBehaviour
{
    public SpellInteractionBindings mInteractionBindings;
    public int forceScalar;

    private Rigidbody2D thisRigidbody;

    private void Start()
    {
        mInteractionBindings.SubscribeInteractionEvent(ForceAction);
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    private void ForceAction(ISpellInteractionType eventType, Vector2 eventSource)
    {
        if (eventType is ForceInteraction forceEvent && Mathf.Abs(forceEvent.mForceStrength) > 1)
        {
            thisRigidbody.AddForceAtPosition(forceEvent.mForceStrength * forceScalar * ((Vector2)transform.position - eventSource).normalized, eventSource);
        }
    }
}
