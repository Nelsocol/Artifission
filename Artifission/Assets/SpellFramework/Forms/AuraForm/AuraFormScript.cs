using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraFormScript : MonoBehaviour, ISpellForm
{
    public GameObject auraObject;

    public void EndTrigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
    }

    public void InitializeSpell(ISpellEffect spellEffect, Transform casterTransform)
    {
    }

    public bool IsContinuous()
    {
        return false;
    }

    public void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        GameObject newAura = Instantiate(auraObject, casterTransform);
        ParticleSystem[] auraParticles = newAura.GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particleSystem in auraParticles)
        {
            spellEffect.ApplyParticleSystemEffectors(particleSystem);
        }
        AuraBehaviorScript auraBehavior = newAura.GetComponent<AuraBehaviorScript>();
        auraBehavior.spellEffect = spellEffect;
        auraBehavior.casterTransform = casterTransform;
        auraBehavior.hitData = hitData;
    }

    public bool WindUp()
    {
        return true;
    }
}
