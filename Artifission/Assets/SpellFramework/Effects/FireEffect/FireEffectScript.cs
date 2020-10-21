using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffectScript : MonoBehaviour, ISpellEffect
{
    public ParticleSystem.MinMaxGradient colorOverride;
    public ParticleSystem.MinMaxGradient lifetimeColorOverride;
    public float gravityOverride;
    public float noiseModifier;

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
}
