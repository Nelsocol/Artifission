using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchScript : MonoBehaviour
{
    public SpellInteractionBindings interactionBindings;
    public Light2D lightReference;
    public ParticleSystem particleReference;
    public bool lit = false;
    public float sustainTime = 0;
    public bool mustLight;
    public bool mustNotLight;
    public TorchScript dependsOn;

    private float litTimer;
    private TorchControllerScript controller;

    private void Start()
    {
        interactionBindings.SubscribeInteractionEvent(IgniteAction);
        controller = GetComponentInParent<TorchControllerScript>();
    }

    private void Update()
    {
        if(lit && sustainTime > 0 && litTimer <= 0)
        {
            Extinguish();
        }
        else
        {
            litTimer -= Time.deltaTime;
        }
    }

    public void Light()
    {
        lightReference.intensity = 1;
        particleReference.Play();
        litTimer = sustainTime;
        lit = true;

        if((dependsOn != null && !dependsOn.lit) || mustNotLight)
        {
            controller?.ExtinguishAll();
        }
    }

    public void Extinguish()
    {
        lightReference.intensity = 0;
        particleReference.Stop();
        lit = false;
    }

    private void IgniteAction(ISpellInteractionType interaction, Vector2 eventSource)
    {
        if (interaction is BurnInteraction)
        {
            Light();
        }
    }
}
