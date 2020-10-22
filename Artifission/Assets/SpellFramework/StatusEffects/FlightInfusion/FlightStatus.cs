using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlightStatus : MonoBehaviour, IStatusEffect
{
    private PlayerMovement playerMovement;

    public float verticalDriftStrength;
    public float statusTime;
    public float horizontalSpeedAffector;

    Rigidbody2D parentRigidbody;
    private float originalGravity;
    private float remainingStatusTime;

    private void Start()
    {
        remainingStatusTime = statusTime;
        if(transform.parent.TryGetComponent(out parentRigidbody))
        {
            originalGravity = parentRigidbody.gravityScale;
            parentRigidbody.gravityScale = 0;
            playerMovement = transform.parent.GetComponent<PlayerMovement>();
            playerMovement.walkSpeed /= horizontalSpeedAffector;
        }
    }

    private void FixedUpdate()
    {
        if (parentRigidbody && remainingStatusTime > 0)
        {
            if (Keyboard.current.wKey.IsPressed())
            {
                parentRigidbody.AddForce(new Vector2(0, verticalDriftStrength));
            }
            else if(Keyboard.current.sKey.IsPressed())
            {
                parentRigidbody.AddForce(new Vector2(0, -verticalDriftStrength));
            }
        }
        else
        {
            RemoveEffect(false, false);
        }
        remainingStatusTime -= Time.fixedDeltaTime;
    }

    public void RemoveEffect(bool targetDead, bool hardRemoval)
    {
        if (parentRigidbody != null)
        {
            parentRigidbody.gravityScale = originalGravity;
            playerMovement.walkSpeed *= horizontalSpeedAffector;
        }

        if(!hardRemoval)
        {
            foreach(ParticleSystem particleSystem in GetComponentsInChildren<ParticleSystem>())
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
