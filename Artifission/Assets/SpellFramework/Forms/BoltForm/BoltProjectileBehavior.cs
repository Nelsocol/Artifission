﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoltProjectileBehavior : MonoBehaviour
{
    private Transform thisTransform;

    public float projectileSpeed;
    public GameObject projectileParticles;
    public GameObject burstParticles;
    public UnifiedHitData hitData;
    public float pauseTimeOnHit;
    public ISpellEffect effectScript;

    private Vector2 movementVector;
    private float lifeTime = 3;
    private float stalledTimeRemaining = 0;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        movementVector = ((Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - (Vector2)thisTransform.position).normalized;
    }

    void Update()
    {
        if (stalledTimeRemaining <= 0)
        {
            thisTransform.Translate(movementVector * projectileSpeed * Time.deltaTime);
        }
        else
        {
            stalledTimeRemaining -= Time.deltaTime;
        }

        if(lifeTime < 0)
        {
            Destroy(burstParticles);
            thisTransform.DetachChildren();
            Destroy(gameObject);
        }

        lifeTime -= Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.layer != 8 && collision.gameObject.layer != 11)
        {
            effectScript.SpecialOnTriggerAction(transform.position);

            bool enemyKilled = false;
            UniversalCreatureBindings enemyBindings;
            if (collision.gameObject.TryGetComponent(out enemyBindings))
            {
                hitData.hitSource = thisTransform.position;
                enemyKilled = enemyBindings.TakeHit(hitData, 1);
                effectScript.SpecialOnHitAction(collision.gameObject, hitData);
            }

            foreach (ParticleSystem particleSystem in burstParticles.GetComponents<ParticleSystem>())
            {
                particleSystem.Play();
            }

            if (!enemyKilled)
            {
                foreach (ParticleSystem particleSystem in projectileParticles.GetComponents<ParticleSystem>())
                {
                    ParticleSystem.EmissionModule particleEmission = particleSystem.emission;
                    particleEmission.enabled = false;
                }

                foreach(ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>())
                {
                    particleSystem.transform.parent = null;
                }

                Destroy(gameObject);
            }
            else
            {
                stalledTimeRemaining = pauseTimeOnHit;
            }
        }

        SpellInteractionBindings interactionBindings;
        if (collision.gameObject.TryGetComponent<SpellInteractionBindings>(out interactionBindings))
        {
            foreach(ISpellInteractionType interaction in hitData.hitInteractions)
            {
                interactionBindings.RaiseInteractionEvent(interaction, transform.position);
            }
        }
    }
}
