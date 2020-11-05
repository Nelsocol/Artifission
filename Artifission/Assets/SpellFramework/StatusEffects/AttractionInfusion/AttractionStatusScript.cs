using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttractionStatusScript : MonoBehaviour, IStatusEffect
{
    private Transform playerStats = null;
    private float statusTime;
    private float remainingStatusTime;
    public float stickForce;
    private float powerAffector;

    private void Start()
    {
        playerStats = transform.parent;
    }

    private void FixedUpdate()
    {
        Collider2D[] nearbyBodies = Physics2D.OverlapCircleAll(playerStats.position, (5 * powerAffector));
        Rigidbody2D physicsBody;
        foreach (Collider2D body in nearbyBodies)
        {
            if (body.TryGetComponent(out physicsBody))
            {
                if (physicsBody.tag != "Player")
                {
                    float adjustedAttractionStrength = stickForce * (((Vector2)playerStats.position - physicsBody.position).magnitude / (5 * powerAffector));
                    physicsBody.AddForce(((Vector2)playerStats.position - physicsBody.position).normalized * adjustedAttractionStrength, ForceMode2D.Impulse);
                    physicsBody.velocity *= 0.2f;
                }
            }
        }
        remainingStatusTime -= Time.fixedDeltaTime;
    }

    public void RemoveEffect(bool targetDead, bool hardRemoval)
    {

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
        statusTime = duration;
        this.powerAffector = powerAffector;
        remainingStatusTime = statusTime;
    }
}
