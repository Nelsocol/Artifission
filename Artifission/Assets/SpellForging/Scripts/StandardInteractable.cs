using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class StandardInteractable : MonoBehaviour
{
    public SpriteRenderer interactibilityIndicator;

    protected bool eligibleForInteraction = false;

    protected GameObject storedPlayerBindings;

    public virtual void Start()
    {
        interactibilityIndicator.enabled = false;    
    }

    void Update()
    {
        if(eligibleForInteraction && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact(storedPlayerBindings);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatBindings playerTest;
        if (collision.gameObject.TryGetComponent(out playerTest))
        {
            interactibilityIndicator.enabled = true;
            eligibleForInteraction = true;
            storedPlayerBindings = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactibilityIndicator.enabled = false;
        eligibleForInteraction = false;
    }

    protected virtual void Interact(GameObject player)
    {
    }
}
