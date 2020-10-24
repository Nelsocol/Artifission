using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.InputSystem;

public class BoltFormScript : MonoBehaviour, ISpellForm
{
    public GameObject projectilePrefab;

    public void EndTrigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
    }

    public bool IsContinuous()
    {
        return true;
    }

    public void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        GameObject projectile = Instantiate(projectilePrefab, casterTransform.position, Quaternion.identity);
        BoltProjectileBehavior projectileScript = projectile.GetComponent<BoltProjectileBehavior>();
        projectileScript.hitData = hitData;
        projectileScript.effectScript = spellEffect;


        foreach (ParticleSystem particleSystem in projectile.GetComponentsInChildren<ParticleSystem>())
        {
            spellEffect.ApplyParticleSystemEffectors(particleSystem);
        }

        foreach (Light2D light in projectile.GetComponentsInChildren<Light2D>())
        {
            spellEffect.ApplyLightEffectors(light);
        }
    }
}
