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

    private float elapsedTime = 0;
    private bool removed = false;

    void Start()
    {
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
        if (!removed)
        {
            if (elapsedTime > tickRate)
            {
                enemyBindings.TakeHit(tickHit);
                elapsedTime = 0;
            }
            elapsedTime += Time.fixedDeltaTime;
        }
    }

    public void RemoveEffect(bool targetDead, bool hardRemoval)
    {
        removed = true;
        burnParticles.Stop();

        if(targetDead)
        {
            if (!hardRemoval)
            {
                transform.DetachChildren();
                deathParticles.Play();
            }
        }
        else
        {

            if (enemyBindings is StandardEnemyBindings) (enemyBindings as StandardEnemyBindings).deathParticlesOverriden = false;
        }
        Destroy(gameObject);
    }
}
