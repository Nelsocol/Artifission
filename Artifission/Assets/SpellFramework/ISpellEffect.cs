using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public interface ISpellEffect
{
    void ApplyParticleSystemEffectors(ParticleSystem system);
    void ApplyLightEffectors(Light2D light);
    void SpecialOnTriggerAction(Vector2 triggerLocation);
    GameObject RetrievePositiveEffect();
}