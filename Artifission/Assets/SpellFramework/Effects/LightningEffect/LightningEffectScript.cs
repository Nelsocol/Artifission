using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightningEffectScript : MonoBehaviour, ISpellEffect
{

    #region AppearanceOverrides
    public ParticleSystem.MinMaxGradient colorOverride;
    public ParticleSystem.MinMaxGradient lifetimeColorOverride;
    public float gravityOverride;
    public float noiseModifier;
    public float noiseFrequencyModifier;
    public Color lightColor;
    public float trailLength;
    public float simSpeedMultiplier;
    public float lifeTimeMultiplier;
    public float trailRatio;
    public float emissionOverTimeMultiplier;
    public float bounceMultiplier;
    public float dampenMultiplier;
    public float burstEmissionMultiplier;
    #endregion

    public float shockRadius;

    public GameObject infusionStatus;
    public GameObject arcObject;

    public void ApplyLightEffectors(Light2D light)
    {
        light.color = lightColor;
    }

    public void ApplyParticleSystemEffectors(ParticleSystem system)
    {
        ParticleSystem.MainModule mainSettings = system.main;
        mainSettings.startColor = colorOverride;
        mainSettings.gravityModifier = gravityOverride;
        mainSettings.simulationSpeed *= simSpeedMultiplier;
        mainSettings.startLifetime = new ParticleSystem.MinMaxCurve(mainSettings.startLifetime.constantMin * lifeTimeMultiplier, mainSettings.startLifetime.constantMax * lifeTimeMultiplier);

        ParticleSystem.ColorOverLifetimeModule lifetimeColorSettings = system.colorOverLifetime;
        lifetimeColorSettings.color = lifetimeColorOverride;

        ParticleSystem.NoiseModule noiseSettings = system.noise;
        noiseSettings.strengthMultiplier *= noiseModifier;
        noiseSettings.frequency *= noiseFrequencyModifier;

        ParticleSystem.TrailModule trailSettings = system.trails;
        trailSettings.enabled = true;
        trailSettings.lifetime = trailLength;
        trailSettings.ratio = trailRatio;

        ParticleSystem.EmissionModule emissionSettings = system.emission;
        emissionSettings.rateOverTimeMultiplier *= emissionOverTimeMultiplier;
        ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emissionSettings.burstCount];
        emissionSettings.GetBursts(bursts);
        for(int i = 0; i < emissionSettings.burstCount; i++)
        {
            bursts[i].count = new ParticleSystem.MinMaxCurve(bursts[i].count.constant * burstEmissionMultiplier);
        }
        emissionSettings.SetBursts(bursts);

        ParticleSystem.CollisionModule collisionSettings = system.collision;
        collisionSettings.bounceMultiplier *= bounceMultiplier;
        collisionSettings.dampenMultiplier *= dampenMultiplier;
    }

    public List<ISpellInteractionType> RetrieveInteractionData()
    {
        return new List<ISpellInteractionType>();
    }

    public GameObject RetrievePositiveEffect()
    {
        return infusionStatus;
    }

    public void SpecialOnHitAction(GameObject hitTarget, UnifiedHitData originalHitData)
    {
        UniversalCreatureBindings checkCache;
        Collider2D[] ancillaryTargets = Physics2D.OverlapCircleAll(hitTarget.transform.position, shockRadius);
        Collider2D[] filteredTargets = ancillaryTargets.Where(e => e.gameObject != hitTarget && e.TryGetComponent(out checkCache) && !(checkCache is PlayerStatBindings)).ToArray();
        if(!(filteredTargets.Length == 0))
        {
            filteredTargets[0].gameObject.GetComponent<UniversalCreatureBindings>().TakeHit(originalHitData, 0.5f);
            ArcScript arcScript = Instantiate(arcObject).GetComponent<ArcScript>();
            arcScript.Arc(hitTarget.transform.position, filteredTargets[0].transform.position);
        }
    }

    public void SpecialOnTriggerAction(Vector2 triggerLocation)
    {
    }
}
