using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SimpleEffectScript : MonoBehaviour, ISpellEffect
{
    public ParticleSystem.MinMaxGradient colorOverride;
    public ParticleSystem.MinMaxGradient lifetimeColorOverride;
    public Color lightColor;

    public void ApplyLightEffectors(Light2D light)
    {
        light.color = lightColor;
    }

    public void ApplyParticleSystemEffectors(ParticleSystem system)
    {
        ParticleSystem.MainModule mainSettings = system.main;
        mainSettings.startColor = colorOverride;

        ParticleSystem.ColorOverLifetimeModule lifetimeColorSettings = system.colorOverLifetime;
        lifetimeColorSettings.color = lifetimeColorOverride;
    }
}
