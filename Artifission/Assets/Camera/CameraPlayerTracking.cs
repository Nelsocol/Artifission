using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTracking : MonoBehaviour
{
    private Transform thisTransform;
    private Transform playerTransform;

    public GameObject player;

    void Start()
    {
        thisTransform = GetComponent<Transform>();
        playerTransform = player.GetComponent<Transform>();
    }

    void Update()
    {
        thisTransform.position = new Vector3(playerTransform.position.x, thisTransform.position.y, thisTransform.position.z);
    }
}
