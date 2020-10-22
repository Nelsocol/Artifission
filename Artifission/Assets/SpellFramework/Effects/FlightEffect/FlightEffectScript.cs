using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlightEffectScript : MonoBehaviour, ISpellEffect
{
    public ParticleSystem.MinMaxGradient colorOverride;
    public ParticleSystem.MinMaxGradient lifetimeColorOverride;
    public float gravityOverride;
    public float noiseModifier;
    public Color lightColor;
    public float lifeTimeMultiplier;
    public float speedMultiplier;
    public GameObject infusionStatus;

    public GameObject updraftObject;

    public void ApplyLightEffectors(Light2D light)
    {
        light.color = lightColor;
    }

    public void ApplyParticleSystemEffectors(ParticleSystem system)
    {
        ParticleSystem.MainModule mainSettings = system.main;
        mainSettings.startColor = colorOverride;
        mainSettings.gravityModifier = gravityOverride;
        mainSettings.startLifetime = new ParticleSystem.MinMaxCurve(mainSettings.startLifetime.constantMin * lifeTimeMultiplier, mainSettings.startLifetime.constantMax * lifeTimeMultiplier);
        mainSettings.startSpeed = new ParticleSystem.MinMaxCurve(mainSettings.startSpeed.constantMin * speedMultiplier, mainSettings.startSpeed.constantMax * speedMultiplier);

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
        Instantiate(updraftObject, triggerLocation += new Vector2(0, -0.3f), Quaternion.identity);
    }
}
