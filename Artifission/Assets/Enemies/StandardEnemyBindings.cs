using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StandardEnemyBindings : MonoBehaviour, UniversalCreatureBindings
{
    private Rigidbody2D thisRigidbody;
    private Transform thisTransform;
    private EnemyNodeScript parentNode;
    private CreatureBrainCore thisBrain;

    public GameObject deathParticles;
    public float maxHP;
    public float weight;
    public bool deathParticlesOverriden;

    private float currentHP;

    public virtual void ReceiveImpact(UnifiedHitData hitData)
    {
        if(hitData.baseImpact > weight)
        {
            thisBrain.SendMessage(StateMessages.Stunned);
        }
    }

    public void Start()
    {
        currentHP = maxHP;
        thisRigidbody = GetComponent<Rigidbody2D>();
        thisTransform = GetComponent<Transform>();
        transform.parent.TryGetComponent(out parentNode);
        thisBrain = GetComponentInChildren<CreatureBrainCore>();
    }

    public virtual bool TakeHit(UnifiedHitData hitData, float hitScalar)
    {
        currentHP -= hitData.baseDamage * hitScalar;

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

        if(parentNode)
        {
            parentNode.instanceKilled = true;
        }

        Destroy(gameObject);
    }

    public virtual void ApplyStatus(GameObject statusEffect, float powerMultiplier = 1, float duration = 5)
    {
        IStatusEffect newStatus;
        if (!GetComponentsInChildren<IStatusEffect>().Any(e => e.GetType() == statusEffect.GetComponent<IStatusEffect>().GetType()))
        {
            newStatus = Instantiate(statusEffect, thisTransform).GetComponent<IStatusEffect>();
            newStatus.SetParameters(powerMultiplier, duration);
        }
        else
        {
            GetComponentsInChildren<IStatusEffect>().First(e => e.GetType() == statusEffect.GetComponent<IStatusEffect>().GetType()).ResetEffect();
        }
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
