using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagicInfusionScript : MonoBehaviour, IStatusEffect
{
    private PlayerStatBindings playerStats = null;

    public float manaRegenMultiplier;
    public float statusTime;

    private float remainingStatusTime;

    private void Start()
    {
        remainingStatusTime = statusTime;
        if (transform.parent.TryGetComponent(out playerStats))
        {
            playerStats.constantManaRegeneration *= manaRegenMultiplier;
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
            playerStats.constantManaRegeneration /= manaRegenMultiplier;
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
