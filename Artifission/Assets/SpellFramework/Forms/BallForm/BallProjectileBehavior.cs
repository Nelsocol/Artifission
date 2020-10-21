﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallProjectileBehavior : MonoBehaviour
{
    private Rigidbody2D thisRigidbody;
    private Transform thisTransform;
    private Camera mainCamera;

    public float throwForce;
    public GameObject projectileParticles;
    public GameObject burstParticles;
    public UnifiedHitData hitData;
    public float explosionRadius;

    private Vector2 movementVector;

    void Start()
    {
        mainCamera = Camera.main;
        thisTransform = GetComponent<Transform>();
        thisRigidbody = GetComponent<Rigidbody2D>();
        movementVector = ((Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - thisRigidbody.position).normalized;
        thisRigidbody.AddForce(throwForce * movementVector, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            List<GameObject> handledEnemies = new List<GameObject>();
            foreach(Collider2D potentialTarget in Physics2D.OverlapCircleAll(thisTransform.position, explosionRadius))
            { 
                UniversalEnemyBindings enemyBindings;
                if(potentialTarget.TryGetComponent(out enemyBindings) && !(enemyBindings is PlayerStatBindings) && !handledEnemies.Contains(potentialTarget.gameObject))
                {
                    RaycastHit2D obstructionCheck = Physics2D.Raycast(thisTransform.position, (potentialTarget.GetComponent<Transform>().position - thisTransform.position));
                    if (obstructionCheck && obstructionCheck.collider.gameObject == potentialTarget.gameObject)
                    {
                        hitData.hitSource = thisTransform.position;
                        enemyBindings.TakeHit(hitData);
                        handledEnemies.Add(potentialTarget.gameObject);
                    }
                }
            }

            foreach (ParticleSystem particleSystem in projectileParticles.GetComponents<ParticleSystem>())
            {
                ParticleSystem.EmissionModule particleEmission = particleSystem.emission;
                particleEmission.enabled = false;
            }

            foreach (ParticleSystem particleSystem in burstParticles.GetComponents<ParticleSystem>())
            {
                particleSystem.Play();
            }
            mainCamera.GetComponent<CameraShakeBindings>().AddTremor(0.1f, 0.15f);
            thisTransform.DetachChildren();
            Destroy(gameObject);
        }
    }
}
