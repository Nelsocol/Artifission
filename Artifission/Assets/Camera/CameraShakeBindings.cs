using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeBindings : MonoBehaviour
{
    public CameraPlayerTracking trackingScript;

    private Transform myTransform;

    private float shakeAmount = 0;
    private float elapsedTime = 0;
    private bool outOfPosition;
    private bool shaking = false;

    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if (shaking) {
            if (elapsedTime > 0)
            {
                if (!outOfPosition)
                {
                    transform.position += new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), 0);
                    outOfPosition = true;
                }
                else
                {
                    myTransform.position = new Vector3(0, 0, transform.position.z) + (Vector3)(Vector2)trackingScript.player.transform.position;
                    outOfPosition = false;
                }
                elapsedTime -= Time.fixedDeltaTime;
            }
            else if (outOfPosition)
            {
                myTransform.position = new Vector3(0, 0, transform.position.z) + (Vector3)(Vector2)trackingScript.player.transform.position;
                outOfPosition = false;
                trackingScript.state = CameraState.TRACKING;
                shaking = false;
            }
            else
            {
                trackingScript.state = CameraState.TRACKING;
                shaking = false;
            }
        }
    }

    public void AddTremor(float amount, float timeSpan)
    {
        if (!outOfPosition)
        {
            shakeAmount = amount;
            elapsedTime = timeSpan;
            outOfPosition = true;
            trackingScript.state = CameraState.CUTSCENE;
            shaking = true;
        }
    }
}
