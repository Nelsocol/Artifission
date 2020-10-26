using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStatusScript : MonoBehaviour, IStatusEffect
{
    private PlayerMovement playerStats = null;

    public float speedMultiplier;
    public float statusTime;

    private float remainingStatusTime;

    private void Start()
    {
        remainingStatusTime = statusTime;
        if (transform.parent.TryGetComponent(out playerStats))
        {
            playerStats.walkSpeed *= speedMultiplier;
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
            playerStats.walkSpeed /= speedMultiplier;
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
}
