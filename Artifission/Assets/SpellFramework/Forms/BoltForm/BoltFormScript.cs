using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoltFormScript : MonoBehaviour, ISpellForm
{
    public GameObject projectilePrefab;
    public void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        GameObject projectile = Instantiate(projectilePrefab, casterTransform.position, Quaternion.identity);
        projectile.GetComponent<BoltProjectileBehavior>().hitData = hitData;

        foreach (ParticleSystem particleSystem in projectile.GetComponentsInChildren<ParticleSystem>())
        {
            spellEffect.ApplyParticleSystemEffectors(particleSystem);
        }
    }
}
