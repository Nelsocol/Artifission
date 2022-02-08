using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestructableProjectileBindings : MonoBehaviour, UniversalCreatureBindings
{
    public float durability;

    public virtual void ReceiveImpact(UnifiedHitData hitData)
    {
    }

    public void Start()
    {
    }

    public virtual bool TakeHit(UnifiedHitData hitData, float hitScalar)
    {
        if(hitData.baseDamage * hitScalar > durability)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public virtual void Kill()
    {
    }

    public virtual void ApplyStatus(GameObject statusEffect, float powerMultiplier = 1, float duration = 5)
    {
    }

    public void ClearAllStatus(bool hardRemoval)
    {
    }

    public void ResetStats()
    {
    }

    public float GetParticleEffectScalar()
    {
        return 1;
    }

    public float GetMaxHealth()
    {
        return durability;
    }

    public float GetCurrentHealth()
    {
        return durability;
    }
}
