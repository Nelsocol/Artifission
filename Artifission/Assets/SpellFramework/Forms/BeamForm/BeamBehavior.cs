using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeamBehavior : MonoBehaviour
{
    private LineRenderer coreReference;

    public Transform storedPlayerTransform;
    public bool firing;
    public ParticleSystem beamParticles;
    public ParticleSystem startParticles;
    public ParticleSystem endParticles;
    public float hitInterval;
    public UnifiedHitData hitData;
    public float ancillaryTriggerInterval;
    public ISpellEffect spellEffect;
    public LayerMask layerMask;

    private float timeElapsed = 0;
    private float triggerTimeElapsed;


    void Start()
    {
        coreReference = GetComponent<LineRenderer>();
        coreReference.enabled = false;
        triggerTimeElapsed = ancillaryTriggerInterval;
    }

    void Update()
    {
        coreReference.SetPosition(0, storedPlayerTransform.position);

        RaycastHit2D directionalCast = Physics2D.Raycast(storedPlayerTransform.position, (Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - storedPlayerTransform.position), 50, ~layerMask);

        if (directionalCast)
        {
            coreReference.SetPosition(1, directionalCast.point);

            StandardEnemyBindings potentialEnemyBindings;
            if(timeElapsed > hitInterval && directionalCast.transform.gameObject.TryGetComponent(out potentialEnemyBindings))
            {
                hitData.hitSource = storedPlayerTransform.position;
                potentialEnemyBindings.TakeHit(hitData, 1);
                spellEffect.SpecialOnHitAction(potentialEnemyBindings.gameObject, hitData);
                timeElapsed = 0;
            }

            if(triggerTimeElapsed > ancillaryTriggerInterval)
            {
                spellEffect.SpecialOnTriggerAction(coreReference.GetPosition(1));
                triggerTimeElapsed = 0;
            }
        }

        ParticleSystem.ShapeModule beamShape = beamParticles.shape;
        Vector3 startPos = coreReference.GetPosition(0);
        Vector3 endPos = coreReference.GetPosition(1);
        beamShape.position = (startPos + endPos) / 2;
        beamShape.radius = (endPos - startPos).magnitude / 2;
        beamShape.rotation = new Vector3(0, 0, Mathf.Atan2(endPos.y - startPos.y, endPos.x - startPos.x) * Mathf.Rad2Deg);

        startParticles.transform.position = startPos;
        endParticles.transform.position = endPos;

        timeElapsed += Time.deltaTime;
        triggerTimeElapsed += Time.deltaTime;
        InitialActivation();
    }

    private void InitialActivation()
    {

        if (coreReference.enabled == false)
        {
            coreReference.enabled = true;
        }

        foreach (ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>())
        {
            if (!particleSystem.isPlaying)
            {
                particleSystem.Play();
            }
        }
    }
}
