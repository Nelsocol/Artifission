using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class OrbitalScript : MonoBehaviour, ISpellForm
{
    public GameObject beamObject;
    public GameObject targetObject;
    public float explosionRadius;
    public LayerMask layerMask;
    public float targetTime;

    private Vector2 mousePosition;
    private RaycastHit2D landingPoint;
    private RaycastHit2D hitPoint;
    private ParticleSystem targetParticles;
    private float elapsedTargetTime;

    private void Start()
    {
    }

    public void EndTrigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
    }

    public bool IsContinuous()
    {
        return false;
    }

    public void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        if (landingPoint && hitPoint && hitPoint.collider.tag != "Ground")
        {
            targetParticles.Stop();
            Camera.main.GetComponent<CameraShakeBindings>().AddTremor(0.5f, 0.2f);
            GameObject beam = Instantiate(beamObject, landingPoint.point, Quaternion.identity);
            foreach (ParticleSystem particleSystem in beam.GetComponentsInChildren<ParticleSystem>())
            {
                spellEffect.ApplyParticleSystemEffectors(particleSystem);
            }
            spellEffect.SpecialOnTriggerAction(landingPoint.point + new Vector2(0, 0.1f));

            Collider2D[] nearbyBodies = Physics2D.OverlapCircleAll(landingPoint.point, explosionRadius);
            List<GameObject> handledEnemies = new List<GameObject>();
            StandardEnemyBindings enemyBindings = null;
            foreach (Collider2D body in nearbyBodies.Where(e => e.TryGetComponent(out enemyBindings) && !(handledEnemies.Contains(e.gameObject))))
            {
                //if (Physics2D.Raycast(landingPoint.point, ((Vector2)body.gameObject.transform.position - landingPoint.point).normalized).collider.tag != "Ground")
                //{
                    enemyBindings.TakeHit(hitData, 1);
                    spellEffect.SpecialOnHitAction(body.gameObject, hitData);
                    handledEnemies.Add(body.gameObject);
                //}
            }

            foreach(Collider2D body in nearbyBodies)
            {
                PropScript propScript;
                if (body.TryGetComponent(out propScript))
                {
                    propScript.ReceiveImpact(landingPoint.point, 7);
                }

                SpellInteractionBindings interactionBindings;
                if (body.TryGetComponent(out interactionBindings))
                {
                    foreach (ISpellInteractionType interaction in hitData.hitInteractions)
                    {
                        Debug.Log("RaisedEvent");
                        interactionBindings.RaiseInteractionEvent(interaction, landingPoint.point);
                    }
                }    
            }

            RaycastHit2D[] enemiesInBeam = new RaycastHit2D[20];
            Physics2D.RaycastNonAlloc(landingPoint.point, Vector2.up, enemiesInBeam, 60);
            foreach (RaycastHit2D hit in enemiesInBeam.Where(e => e && !(handledEnemies.Contains(e.collider.gameObject)) && e.collider.gameObject.TryGetComponent(out enemyBindings)))
            {
                enemyBindings.TakeHit(hitData, 1);
                spellEffect.SpecialOnHitAction(hit.collider.gameObject, hitData);
                handledEnemies.Add(hit.collider.gameObject);
            }
        }
    }

    public void InitializeSpell(ISpellEffect spellEffect, Transform casterTransform)
    {
        mousePosition = Mouse.current.position.ReadValue();
        landingPoint = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), Vector2.down, 40, ~layerMask);
        hitPoint = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePosition), casterTransform.position - Camera.main.ScreenToWorldPoint(mousePosition), 40, ~layerMask);

        if (landingPoint && hitPoint && hitPoint.collider.tag != "Ground")
        {
            targetParticles = Instantiate(targetObject, landingPoint.point, Quaternion.identity).GetComponent<ParticleSystem>();
            spellEffect.ApplyParticleSystemEffectors(targetParticles);
            targetParticles.Play();
        }
        elapsedTargetTime = 0;
    }

    public bool WindUp()
    {
        elapsedTargetTime += Time.deltaTime;
        return (elapsedTargetTime > targetTime);
    }
}
