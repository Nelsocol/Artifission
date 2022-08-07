using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TempLightningScript : MonoBehaviour
{
    public float flashDuration;
    public float flashDelayMin;
    public float flashDelayMax;

    private float timeRemaining;
    private Light2D lightReference;
    private float baseIntensity;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = Random.value * (flashDelayMax - flashDelayMin) + flashDelayMin;
        lightReference = GetComponent<Light2D>();
        baseIntensity = lightReference.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining < 0 && lightReference.enabled)
        {
            lightReference.enabled = false;
            timeRemaining = Random.value * (flashDelayMax - flashDelayMin) + flashDelayMin;
            lightReference.intensity = baseIntensity;
        }
        else if (timeRemaining < 0 && !lightReference.enabled)
        {
            lightReference.enabled = true;
            timeRemaining = flashDuration;
        }
        else {
            if (lightReference.enabled) 
            {
                lightReference.intensity *= Random.value + 0.5f;
            }

            timeRemaining -= Time.deltaTime;
        }
    }
}
