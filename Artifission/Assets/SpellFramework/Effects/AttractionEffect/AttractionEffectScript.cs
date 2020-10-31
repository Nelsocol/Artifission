using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class AttractionEffectScript : MonoBehaviour, ISpellEffect
{
    public ParticleSystem.MinMaxGradient colorOverride;
    public ParticleSystem.MinMaxGradient lifetimeColorOverride;
    public float gravityOverride;
    public float noiseModifier;
    public float noiseFrequencyModifier;
    public Color lightColor;
    public GameObject infusionStatus;
    public float particleSpeedMod;
    public ParticleSystem.MinMaxCurve particleSizeOverLifetime;
    public float baseParticleSize;
    public float dampingThresholdOverride;
    public float dampingStrengthOverride;

    public GameObject attractionZoneAsset;
    public float attractionStrength;

    public void ApplyLightEffectors(Light2D light)
    {
        light.color = lightColor;
    }

    public void ApplyParticleSystemEffectors(ParticleSystem system)
    {
        ParticleSystem.MainModule mainSettings = system.main;
        mainSettings.startColor = colorOverride;
        mainSettings.gravityModifier = gravityOverride;
        mainSettings.startSpeed = new ParticleSystem.MinMaxCurve(mainSettings.startSpeed.constantMin * particleSpeedMod, mainSettings.startSpeed.constantMax * particleSpeedMod);
        mainSettings.startSize = new ParticleSystem.MinMaxCurve(mainSettings.startSize.constantMin * baseParticleSize, mainSettings.startSize.constantMax * baseParticleSize);

        ParticleSystem.ColorOverLifetimeModule lifetimeColorSettings = system.colorOverLifetime;
        lifetimeColorSettings.color = lifetimeColorOverride;

        ParticleSystem.NoiseModule noiseSettings = system.noise;
        noiseSettings.strengthMultiplier *= noiseModifier;
        noiseSettings.frequency *= noiseFrequencyModifier;

        ParticleSystem.SizeOverLifetimeModule sizeOverLifetime = system.sizeOverLifetime;
        sizeOverLifetime.size = particleSizeOverLifetime;

        ParticleSystem.LimitVelocityOverLifetimeModule damping = system.limitVelocityOverLifetime;
        damping.enabled = true;
        damping.limit = dampingThresholdOverride;
        damping.dampen = dampingStrengthOverride;
    }

    public GameObject RetrievePositiveEffect()
    {
        return infusionStatus;
    }

    public void SpecialOnHitAction(GameObject hitTarget, UnifiedHitData originalHitData)
    {
    }

    public void SpecialOnTriggerAction(Vector2 triggerLocation)
    {
        Instantiate(attractionZoneAsset, triggerLocation, Quaternion.identity);
        Collider2D[] nearbyBodies = Physics2D.OverlapCircleAll(triggerLocation, 5);
        Rigidbody2D physicsBody;
        foreach(Collider2D body in nearbyBodies)
        {
            if(body.TryGetComponent(out physicsBody))
            {
                if (physicsBody.tag != "Player")
                {
                    float adjustedAttractionStrength = attractionStrength * ((triggerLocation - physicsBody.position).magnitude / 5);
                    physicsBody.AddForce((triggerLocation - physicsBody.position).normalized * adjustedAttractionStrength, ForceMode2D.Impulse);
                    physicsBody.velocity *= 0.4f;
                }
                else
                {
                    float adjustedAttractionStrength = Mathf.Clamp(attractionStrength / ((triggerLocation - physicsBody.position).magnitude / 5), 0, attractionStrength);
                    physicsBody.AddForce((triggerLocation - physicsBody.position).normalized * attractionStrength / 4, ForceMode2D.Impulse);
                }
            }
        }
    }
}
