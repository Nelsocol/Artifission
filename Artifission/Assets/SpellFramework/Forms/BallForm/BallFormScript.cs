using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BallFormScript : MonoBehaviour, ISpellForm
{
    public GameObject ballProjectile;

    public void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        GameObject projectile = Instantiate(ballProjectile, casterTransform.position, Quaternion.identity);
        BallProjectileBehavior projectileBehavior = projectile.GetComponent<BallProjectileBehavior>();
        projectileBehavior.hitData = hitData;
        projectileBehavior.effectScript = spellEffect;

        foreach(ParticleSystem particleSystem in projectile.GetComponentsInChildren<ParticleSystem>()){
            spellEffect.ApplyParticleSystemEffectors(particleSystem);
        }

        foreach(Light2D light in projectile.GetComponentsInChildren<Light2D>())
        {
            spellEffect.ApplyLightEffectors(light);
        }
    }
}
