using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireEffectScript : MonoBehaviour, ISpellEffect
{
    public ParticleSystem.MinMaxGradient colorOverride;
    public ParticleSystem.MinMaxGradient lifetimeColorOverride;
    public float gravityOverride;
    public float noiseModifier;
    public Color lightColor;
    public GameObject infusionStatus;

    public void ApplyLightEffectors(Light2D light)
    {
        light.color = lightColor;
    }

    public void ApplyParticleSystemEffectors(ParticleSystem system)
    {
        ParticleSystem.MainModule mainSettings = system.main;
        mainSettings.startColor = colorOverride;
        mainSettings.gravityModifier = gravityOverride;

        ParticleSystem.ColorOverLifetimeModule lifetimeColorSettings = system.colorOverLifetime;
        lifetimeColorSettings.color = lifetimeColorOverride;

        ParticleSystem.NoiseModule noiseSettings = system.noise;
        noiseSettings.strengthMultiplier *= noiseModifier;
    }

    public GameObject RetrievePositiveEffect()
    {
        return infusionStatus;
    }

    public void SpecialOnTriggerAction(Vector2 triggerLocation)
    {
    }
}
