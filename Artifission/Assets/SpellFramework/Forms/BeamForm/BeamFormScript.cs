using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeamFormScript : MonoBehaviour, ISpellForm
{
    public GameObject beamObject;
    private GameObject currentBeam;
    private BeamBehavior beamScript;

    public void EndTrigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        if (currentBeam)
        {
            foreach (ParticleSystem particleSystem in currentBeam.GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Stop();
                particleSystem.transform.parent = null;
            }
            Destroy(currentBeam);
            beamScript.firing = false;
        }
    }

    public void InitializeSpell(ISpellEffect spellEffect, Transform casterTransform)
    {
    }

    public bool IsContinuous()
    {
        return true;
    }

    public void Trigger(ISpellEffect spellEffect, Transform casterTransform, UnifiedHitData hitData)
    {
        currentBeam = Instantiate(beamObject, Vector3.zero, Quaternion.identity);
        beamScript = currentBeam.GetComponent<BeamBehavior>();
        beamScript.firing = true;
        beamScript.storedPlayerTransform = casterTransform;
        beamScript.hitData = hitData;
        beamScript.spellEffect = spellEffect;

        foreach(ParticleSystem particleSystem in currentBeam.GetComponentsInChildren<ParticleSystem>())
        {
            spellEffect.ApplyParticleSystemEffectors(particleSystem);
        }
    }

    public bool WindUp()
    {
        return true;
    }
}
