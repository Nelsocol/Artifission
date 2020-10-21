using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemyBindings : MonoBehaviour, UniversalCreatureBindings
{
    private Rigidbody2D thisRigidbody;
    private Transform thisTransform;

    public GameObject deathParticles;
    public float maxHP;
    public float weight;
    public bool deathParticlesOverriden;

    private float currentHP;

    public virtual void ReceiveImpact(UnifiedHitData hitData)
    {
        if(hitData.baseImpact > weight)
        {
            thisRigidbody.AddForce((thisRigidbody.position - hitData.hitSource).normalized * hitData.baseImpact, ForceMode2D.Impulse);
        }
    }

    public void Start()
    {
        currentHP = maxHP;
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisTransform = GetComponent<Transform>();
    }

    public virtual bool TakeHit(UnifiedHitData hitData)
    {
        currentHP -= hitData.baseDamage;

        if (currentHP <= 0)
        {
            Kill();
            return true;
        }

        ReceiveImpact(hitData);

        foreach(GameObject statusEffect in hitData.statusEffects)
        {
            float statusRoll = (Random.Range(0, 100) / 100f);
            if (statusRoll < hitData.statusChance)
            {
                ApplyStatus(statusEffect);
            }
        }
        return false;
    }

    public virtual void Kill()
    {
        foreach(IStatusEffect effect in GetComponentsInChildren<IStatusEffect>())
        {
            effect.RemoveEffect(true, false);
        }

        foreach(ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.gameObject.GetComponent<Transform>().parent = null;
        }

        if (!deathParticlesOverriden)
        {
            deathParticles.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            Destroy(deathParticles);
        }
        Destroy(gameObject);
    }

    public virtual void ApplyStatus(GameObject statusEffect)
    {
        Instantiate(statusEffect, thisTransform);
    }

    public virtual float GetParticleEffectScalar()
    {
        return 1;
    }

    public float GetMaxHealth()
    {
        return maxHP;
    }

    public float GetCurrentHealth()
    {
        return currentHP;
    }

    public void ClearAllStatus(bool hardRemoval)
    {
        foreach (IStatusEffect effect in GetComponentsInChildren<IStatusEffect>())
        {
            effect.RemoveEffect(true, hardRemoval);
        }
    }

    public void ResetStats()
    {
        currentHP = maxHP;
    }
}
