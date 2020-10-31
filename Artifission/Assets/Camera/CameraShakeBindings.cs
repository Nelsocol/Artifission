using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeBindings : MonoBehaviour
{
    public CameraPlayerTracking trackingScript;

    private Transform myTransform;

    private float shakeAmount = 0;
    private float elapsedTime = 0;
    private Vector3 originalPosition;
    private bool outOfPosition;

    void Start()
    {
        myTransform = GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if(elapsedTime > 0)
        {
            if (!outOfPosition)
            {
                transform.position += new Vector3(Random.Range(-shakeAmount, shakeAmount), Random.Range(-shakeAmount, shakeAmount), 0);
                outOfPosition = true;
            }
            else
            {
                myTransform.position = originalPosition;
                outOfPosition = false;
            }
            elapsedTime -= Time.fixedDeltaTime;
        }
        else if(outOfPosition)
        {
            myTransform.position = originalPosition;
            outOfPosition = false;
            trackingScript.tracking = true;
        }
        else
        {
            trackingScript.tracking = true;
        }
    }

    public void AddTremor(float amount, float timeSpan)
    {
        if (!outOfPosition)
        {
            originalPosition = myTransform.position;
            shakeAmount = amount;
            elapsedTime = timeSpan;
            outOfPosition = true;
            trackingScript.tracking = false;
        }
    }
}
