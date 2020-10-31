using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTracking : MonoBehaviour
{
    private Transform thisTransform;
    private Transform playerTransform;

    public GameObject player;
    public bool tracking = true;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
    }

    void Update()
    {
        if (tracking)
        {
            thisTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, thisTransform.position.z);
        }
    }
}
