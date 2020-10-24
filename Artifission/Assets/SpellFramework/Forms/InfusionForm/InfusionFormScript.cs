using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class InfusionFormScript : MonoBehaviour, ISpellForm
{
    public GameObject projectilePrefab;

    public void EndTrigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
    }

    public bool IsContinuous()
    {
        return false;
    }

    public void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        casterTransform.GetComponentInParent<PlayerStatBindings>().ApplyStatus(spellEffect.RetrievePositiveEffect());
    }
}
