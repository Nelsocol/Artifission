using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireInfusionScript : MonoBehaviour, IStatusEffect
{
    private PlayerMovement playerStats = null;

    public float speedMultiplier;
    private float statusTime;
    private float power;

    private float remainingStatusTime;

    private void Start()
    {
        remainingStatusTime = statusTime;
        if (transform.parent.TryGetComponent(out playerStats))
        {
            playerStats.walkSpeed *= (speedMultiplier * power);
        }
    }

    private void FixedUpdate()
    {
        if (!playerStats || remainingStatusTime < 0)
        {
            RemoveEffect(false, false);
        }
        remainingStatusTime -= Time.fixedDeltaTime;
    }

    public void RemoveEffect(bool targetDead, bool hardRemoval)
    {
        if (playerStats != null)
        {
            playerStats.walkSpeed /= (speedMultiplier * power);
        }

        if (!hardRemoval)
        {
            foreach (ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Stop();
                particleSystem.transform.parent = null;
            }
        }

        Destroy(gameObject);
    }

    public void ResetEffect()
    {
        remainingStatusTime = statusTime;
    }

    public void SetParameters(float powerAffector, float duration)
    {
        power = powerAffector;
        statusTime = duration;
        remainingStatusTime = statusTime;
    }
}
