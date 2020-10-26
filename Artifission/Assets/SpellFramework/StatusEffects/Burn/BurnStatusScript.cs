using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnStatusScript : MonoBehaviour, IStatusEffect
{
    private UniversalCreatureBindings enemyBindings;
    private UnifiedHitData tickHit;

    public ParticleSystem burnParticles;
    public ParticleSystem deathParticles;

    public float tickRate;
    public float statusTime;

    private float elapsedTime = 0;
    private bool removed = false;
    private float remainingStatusTime;

    void Start()
    {
        remainingStatusTime = statusTime;
        tickHit = GetComponent<UnifiedHitData>();
        enemyBindings = GetComponentInParent<UniversalCreatureBindings>();
        if (enemyBindings is StandardEnemyBindings) (enemyBindings as StandardEnemyBindings).deathParticlesOverriden = true;

        ParticleSystem.ShapeModule particleShape = burnParticles.shape;
        particleShape.radius = enemyBindings.GetParticleEffectScalar();
        burnParticles.Play();

        ParticleSystem.ShapeModule deathParticleShape = deathParticles.shape;
        deathParticleShape.radius = enemyBindings.GetParticleEffectScalar();

    }

    void FixedUpdate()
    {
        if (!removed && remainingStatusTime > 0)
        {
            if (elapsedTime > tickRate)
            {
                enemyBindings.TakeHit(tickHit, 1);
                elapsedTime = 0;
            }
            elapsedTime += Time.fixedDeltaTime;
        }
        else
        {
            RemoveEffect(false, false);
        }
        remainingStatusTime -= Time.fixedDeltaTime;
    }

    public void RemoveEffect(bool targetDead, bool hardRemoval)
    {
        removed = true;
        burnParticles.Stop();

        if (!hardRemoval)
        {
            transform.DetachChildren();
            if (targetDead)
            {
                deathParticles.Play();
            }
        }

        if(!targetDead)
        {

            if (enemyBindings is StandardEnemyBindings) (enemyBindings as StandardEnemyBindings).deathParticlesOverriden = false;
        }
        Destroy(gameObject);
    }

    public void ResetEffect()
    {
        remainingStatusTime = statusTime;
    }
}
