using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState {
    TRACKING,
    CUTSCENE
}

public class CameraPlayerTracking : MonoBehaviour
{
    private Transform thisTransform;
    private Transform playerTransform;

    public GameObject player;
    public CameraState state = CameraState.TRACKING;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
    }

    void Update()
    {
        if (state == CameraState.TRACKING)
        {
            thisTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, thisTransform.position.z);
        }
    }
}
