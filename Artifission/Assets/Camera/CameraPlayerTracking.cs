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
    public float fixedYPlane;

    public GameObject player;
    public CameraState state = CameraState.TRACKING;
    public float heightOffset;
    public bool yFixed;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
        fixedYPlane = player.transform.position.y;
    }

    void Update()
    {
        if (state == CameraState.TRACKING)
        {
            thisTransform.position = new Vector3(playerTransform.position.x, yFixed ? fixedYPlane + heightOffset : playerTransform.position.y + heightOffset, thisTransform.position.z);
        }
    }

    public void RecenterCamera() 
    {
        fixedYPlane = player.transform.position.y;
    }
}
